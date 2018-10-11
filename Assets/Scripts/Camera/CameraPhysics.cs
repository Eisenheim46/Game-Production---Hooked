using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPhysics : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {

            GetComponent<Rigidbody>().isKinematic = true;
            //falling = false;
        }
    }

}
