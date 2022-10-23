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

    void Awake()
    {
        guns = new List<Gun>();
        gunMap = new Dictionary<string, Gun>();
        foreach (Gun gun in allGuns) {
            gunMap.Add(gun.name, gun);
        }

        EquipGun("LaserCannon");
        //EquipGun("SuperGun");
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
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

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
        if (context.performed)
        {
            guns[gunIdx].Fire();
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
}
