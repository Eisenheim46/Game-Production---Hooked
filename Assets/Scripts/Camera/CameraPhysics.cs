using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPhysics : MonoBehaviour {

    private Rigidbody cameraRb;

    private bool onFloor;

    private void Start()
    {
        cameraRb = GetComponent<Rigidbody>();
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        { 
            cameraRb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        cameraRb.isKinematic = false;
    }

}
