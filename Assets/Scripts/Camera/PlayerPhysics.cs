using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    private Rigidbody cameraRb;

    private bool onFloor;

    public bool OnFloor { get { return onFloor; } } 

    private void Start()
    {
        cameraRb = GetComponent<Rigidbody>();

        //onFloor = true;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        { 
            cameraRb.isKinematic = true;
            onFloor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        cameraRb.isKinematic = false;
        onFloor = false;
    }

}
