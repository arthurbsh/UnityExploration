using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationPower = .3f;

    public GameObject followTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("MB0") > 0 || Input.GetAxis("MB1") > 0) {
            float xAngle = followTransform.transform.localEulerAngles.x;
            float yAngle = followTransform.transform.localEulerAngles.y;

            xAngle += Input.GetAxis("Mouse Y") * rotationPower * -1;
            yAngle += Input.GetAxis("Mouse X") * rotationPower;

            //Clamp the Up/Down rotation
            if (xAngle > 180 && xAngle < 345)
            {
                xAngle = 345;
            }
            else if(xAngle < 180 && xAngle > 89.5)
            {
                xAngle = 89.5f;
            }

            followTransform.transform.localEulerAngles = new Vector3(xAngle, yAngle, 0);
        }    
    }
}
