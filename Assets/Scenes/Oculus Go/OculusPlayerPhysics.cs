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



    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Floor" && !TriggerPressed)
        {
            OnFloor = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ceiling" && !TriggerPressed)
        {
            OnCeiling = true;

            Vector3 currentVelocity = playerRb.velocity;

            currentVelocity.y = 0;

            playerRb.velocity = currentVelocity;
        }
        else if (collision.gameObject.tag == "Wall" && !TriggerPressed)
        {
            OnWall = true;

            playerRb.velocity = Vector3.zero;
            Debug.Log("Called");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        playerRb.useGravity = true;
        playerRb.isKinematic = false;
        OnFloor = false;
    }

}
