using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPhysics : MonoBehaviour {

    private Rigidbody cameraRb;

    private void Start()
    {
        cameraRb = GetComponent<Rigidbody>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {

            cameraRb.isKinematic = true;
            //falling = false;
        }
    }

}
