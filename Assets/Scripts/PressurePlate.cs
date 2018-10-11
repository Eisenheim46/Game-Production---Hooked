using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [SerializeField] private int requiredMass;
    [SerializeField] private bool staysActivated;

    [SerializeField] private GameObject targetObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Metal" || collision.gameObject.tag == "Wooden")
        {
            if (collision.rigidbody.mass == requiredMass)
            {
                targetObject.GetComponent<IsEnabled>().isEnabled = true;
            }   
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (staysActivated == false)
        {
            targetObject.GetComponent<IsEnabled>().isEnabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            targetObject.GetComponent<IsEnabled>().isEnabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targetObject.GetComponent<IsEnabled>().isEnabled = false;
    }
}
