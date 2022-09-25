using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    
    private int gunIdx;

    public List<Gun> guns;

    void awake()
    {
        gunIdx = 0;
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
}
