using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusPlayerPhysics : MonoBehaviour {

    private Rigidbody playerRb;

    public bool TriggerPressed { get; set;}

    public bool OnFloor { get; private set; }

    public bool OnCeiling { get; private set; }

    public bool OnWall { get; private set; }


    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Floor" && !TriggerPressed)
        {
            OnFloor = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ceiling" && !TriggerPressed)
        {
            OnCeiling = true;

            Vector3 currentVelocity = playerRb.velocity;

            currentVelocity.y = 0;

            playerRb.velocity = currentVelocity;
        }
        else if (other.gameObject.tag == "Wall" && !TriggerPressed)
        {
            OnWall = true;

            playerRb.velocity = Vector3.zero;
            Debug.Log("Called");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerRb.useGravity = true;
        playerRb.isKinematic = false;
        OnFloor = false;
    }

}
