using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class P2Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    private Dictionary<string, Gun> gunMap;
    private int gunIdx;

    [SerializeField]
    private List<Gun> guns;

    // All guns in the game
    public List<Gun> allGuns;

    public GameObject frontArm;
    public GameObject backArm;

    public GameObject ReloadCanvas;
    private GameObject reloadCanvas;
    private Image reloadProgress;
    private bool isReloading;

    void Awake()
    {
        guns = new List<Gun>();
        gunMap = new Dictionary<string, Gun>();
        foreach (Gun gun in allGuns) {
            gunMap.Add(gun.name, gun);
        }

        EquipGun("BasicGun");
        gunIdx = 0;
        guns[gunIdx].gameObject.SetActive(true);
        guns[gunIdx].gunActive = true;

        isReloading = false;
        reloadCanvas = Instantiate(ReloadCanvas);
        reloadCanvas.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        foreach(Image i in reloadCanvas.GetComponentsInChildren<Image>())
        {
            if (i.gameObject.name == "indicator")
                reloadProgress = i;
        }
        reloadCanvas.SetActive(isReloading);
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //gun rotation follows mouse
        Vector3 mouseWorldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mouseWorldPoint + (mainCam.transform.forward * 10.0f);
        Vector2 rotation = (mousePos - transform.position).normalized;
        float rotZ = Vector2.SignedAngle(Vector2.right, rotation);
        transform.eulerAngles = new Vector3(0, 0, rotZ);

        frontArm.transform.eulerAngles = new Vector3(frontArm.transform.rotation.x, frontArm.transform.rotation.y, rotZ - 90);
        backArm.transform.eulerAngles = new Vector3(backArm.transform.rotation.x, backArm.transform.rotation.y, rotZ - 90);

        Debug.DrawRay(mousePos, Vector3.right * 100, Color.red);

        guns[gunIdx].UpdateTimer();

        // Help I don't know how the new input system works
        // Fix this later
        if (Input.GetMouseButton(0) && guns[gunIdx].gunInfo.fireType == GunInfo.FireType.Continuous
            && GameManager.Instance.GameState == GameManager.GameStateType.ActionPhase)
        {
            guns[gunIdx].Fire();
        }

        if (Input.GetButton("Reload") && !isReloading)
        {
            StartCoroutine(Reload());
        }

        reloadCanvas.SetActive(isReloading);
        reloadCanvas.transform.position = transform.position + new Vector3(0.8f, 0.6f);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if(context.performed) {
            if (GameManager.Instance.GameState == GameManager.GameStateType.ActionPhase) {
                //fire
                StopAllCoroutines();
                isReloading = false;
                guns[gunIdx].Fire();
            } else if(GameManager.Instance.GameState == GameManager.GameStateType.BuildPhase 
                    && GameManager.Instance.BuildingManager.GetNumBuildingsInInventory() > 0) {
                //TODO: Add shop closed check
                //place building
                GameManager.Instance.BuildingManager.OnPlaceButton();
            }
        }
    }

    public void ToggleWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StopAllCoroutines();
            isReloading = false;
            guns[gunIdx].gunActive = (false);
            gunIdx = (gunIdx + 1) % guns.Count;
            guns[gunIdx].gunActive = (true);
            Debug.Log("Weapon toggled to " + guns[gunIdx].name);
        }
    }

    public bool EquipGun(string gun) {
        if (!gunMap.ContainsKey(gun)) {
            return false;
        } 
        Gun newGun = Instantiate(gunMap[gun], transform);
        newGun.owner = GameManager.Instance.playerShooter;
        newGun.gameObject.SetActive(true);
        newGun.gunActive = false;
        guns.Add(newGun);
        return true;
    }

    // reload script
    public IEnumerator Reload()
    {
        if (guns[gunIdx].heldAmmo == 0 ||
                guns[gunIdx].currentAmmo == guns[gunIdx].gunInfo.clipSize)
            yield return new WaitForEndOfFrame();

        else
        {
            isReloading = true;

            float t = 0;
            while (t < guns[gunIdx].gunInfo.reloadTime)
            {
                reloadProgress.fillAmount = t / guns[gunIdx].gunInfo.reloadTime;
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            uint reloadAmount = (uint)Mathf.Min(guns[gunIdx].gunInfo.clipSize - guns[gunIdx].currentAmmo,
                                            guns[gunIdx].heldAmmo);

            if (guns[gunIdx].heldAmmo < int.MaxValue)
                guns[gunIdx].heldAmmo -= reloadAmount;
            guns[gunIdx].currentAmmo += reloadAmount;

            isReloading = false;
        }
    }

    // public: reloads guns
    public void RestockAllAmmo()
    {
        foreach (Gun gun in guns)
        {
            gun.ReloadGun();
        }
    }

    // for UI use only!
    public Gun GetGun(int gun)
    {
        if (guns.Count <= gun) { return null; }
        gun = (gun + gunIdx) % guns.Count;
        return guns[gun];
    }
}
