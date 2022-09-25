using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
<<<<<<< HEAD

    public GameObject bullet;
    public Transform gun;
    public bool canFire = true;
    private float timer = 0;
    public float timeBetweenFiring;
=======
    
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
>>>>>>> secondary-weapon

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


        //prevent shooting for duration of timeBetweenFiring after a shot
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && canFire)
        {
            canFire = false;
            Instantiate(bullet, gun.position, Quaternion.identity);
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
