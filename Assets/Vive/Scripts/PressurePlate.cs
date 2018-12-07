using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [SerializeField] private int requiredMass;
    [SerializeField] private bool staysActivated;

    [SerializeField] private GameObject targetObject;

    Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Metal" || collision.gameObject.tag == "Wood")
        {
            if (collision.rigidbody.mass >= requiredMass)
            {
                animator.SetBool("IsPressed", true);
                targetObject.GetComponent<IsEnabled>().isEnabled = true;
            }   
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (staysActivated == false)
        {
            animator.SetBool("IsPressed", false);
            targetObject.GetComponent<IsEnabled>().isEnabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("IsPressed", true);
            targetObject.GetComponent<IsEnabled>().isEnabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (staysActivated == false && other.gameObject.tag == "Player")
        {
            animator.SetBool("IsPressed", false);
            targetObject.GetComponent<IsEnabled>().isEnabled = false;
        } 
    }
}
