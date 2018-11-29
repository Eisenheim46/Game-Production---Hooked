using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

    [SerializeField] Hook hook;

    private Rigidbody cameraRb;

    public bool OnFloor { get; private set; }

    public bool OnCeiling { get; private set; }

    public bool OnWall { get; private set; }


    private void Start()
    {
        cameraRb = GetComponent<Rigidbody>();
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Floor" && !hook.GripPressed)
        { 
            cameraRb.isKinematic = true;
            OnFloor = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ceiling" && !hook.GripPressed)
        {
            OnCeiling = true;

            Vector3 currentVelocity = cameraRb.velocity;

            currentVelocity.y = 0;

            cameraRb.velocity = currentVelocity;
        }
        else if (other.gameObject.tag == "Wall" && !hook.GripPressed)
        {
            OnWall = true;

            cameraRb.velocity = Vector3.zero;
            Debug.Log("Called");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        cameraRb.useGravity = true;
        cameraRb.isKinematic = false;
        OnFloor = false;
    }

}
