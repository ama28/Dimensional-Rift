using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        frontArm.transform.eulerAngles = new Vector3(0, 0, rotZ - 90);
        backArm.transform.eulerAngles = new Vector3(0, 0, rotZ - 90);

        Debug.DrawRay(mousePos, Vector3.right * 100, Color.red);

        guns[gunIdx].UpdateTimer();

        // Help I don't know how the new input system works
        // Fix this later
        if (Input.GetMouseButton(0) && guns[gunIdx].gunInfo.fireType == GunInfo.FireType.Continuous)
        {
            guns[gunIdx].Fire();
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if(context.performed) {
            if(GameManager.Instance.GameState == GameManager.GameStateType.ActionPhase && guns[gunIdx].CanFire()) {
                //fire
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
            guns[gunIdx].gameObject.SetActive(false);
            gunIdx = (gunIdx + 1) % guns.Count;
            guns[gunIdx].gameObject.SetActive(true);
            Debug.Log("weapon toggled");
        }
    }

    public bool EquipGun(string gun) {
        if (!gunMap.ContainsKey(gun)) {
            return false;
        } 
        Gun newGun = Instantiate(gunMap[gun], transform);
        newGun.owner = GameManager.Instance.playerShooter;
        newGun.gameObject.SetActive(false);
        guns.Add(newGun);
        return true;
    }

    private void AimBullet(GameObject bullet) { //TODO: add accuracy param
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        Vector3 mouseWorldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = mouseWorldPoint + (mainCam.transform.forward * 10.0f);
        Vector3 direction = mousePos - bullet.transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bullet.GetComponent<Bullet>().speed;

        Vector3 rotation = bullet.transform.position - mousePos;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ + 90);
    }
}
