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

        EquipGun("BasicGun");
        EquipGun("SuperGun");
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
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && guns[gunIdx].CanFire())
        {
            GameObject bullet = guns[gunIdx].Fire();
            Vector3 position = guns[gunIdx].transform.position;
            Instantiate(bullet, position, Quaternion.identity);
            Debug.Log("fired");
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
        newGun.gameObject.SetActive(false);
        guns.Add(newGun);
        return true;
    }
}
