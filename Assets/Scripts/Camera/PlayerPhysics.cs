using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    [SerializeField] Hook hook;

    private Rigidbody cameraRb;

    private bool onFloor;

    public bool OnFloor { get { return onFloor; } } 

    private void Start()
    {
        cameraRb = GetComponent<Rigidbody>();
    }



    private void OnTriggerStay(Collider other)
    {
        Debug.Log(hook.GripPressed);
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "Floor" && !hook.GripPressed)
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
