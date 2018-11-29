using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableChildPhysics : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(Transform child in transform)
            {
                child.GetComponent<Rigidbody>().isKinematic = false;
                child.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
