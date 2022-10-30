using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    private Camera mainCam;
    public enum Target { body, arm, gun}
    public Target target;
    private float pos;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        pos = GameManager.Instance.samXPosition + 1;

        switch (target)
        {
            case Target.body:
                if (mouseWorldPoint.x > pos)
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                else if (mouseWorldPoint.x < pos)
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                break;
            case Target.arm:
                if (mouseWorldPoint.x > pos)
                    GetComponent<SpriteRenderer>().flipY = true;
                else if (mouseWorldPoint.x < pos)
                    GetComponent<SpriteRenderer>().flipY = false;
                break;
            case Target.gun:
                if (mouseWorldPoint.x > pos)
                    GetComponent<SpriteRenderer>().flipY = false;
                else if (mouseWorldPoint.x < pos)
                    GetComponent<SpriteRenderer>().flipY = true;
                break;
        }
    }
}
