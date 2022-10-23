using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFollowMouse : MonoBehaviour
{
    Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseForwardPoint = mouseWorldPoint + (Camera.main.transform.forward * 10.0f); // Replace 10.0f with whatever value
        transform.position = mouseForwardPoint;

    }
}
