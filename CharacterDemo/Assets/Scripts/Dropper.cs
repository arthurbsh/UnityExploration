using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{

    private float initialTime;

    // Start is called before the first frame update
    void Start()
    {
        initialTime = Time.time; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > initialTime + 3) {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
