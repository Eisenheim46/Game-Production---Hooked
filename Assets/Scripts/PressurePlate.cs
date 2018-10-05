using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [SerializeField] private int requiredMass;
    [SerializeField] private bool staysActivated;

    [SerializeField] private GameObject targetDoor;

    private Renderer renderer;
    private Animator animator;



    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    private void Start ()
    {
        
	}
	
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pickable")
        {
            if (collision.rigidbody.mass == requiredMass)
            {
                animator.SetBool("isPressed", true);

                targetDoor.GetComponent<Door>().IsEnabled = true;
            }   
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (staysActivated == false)
        {
            animator.SetBool("isPressed", false);

            targetDoor.GetComponent<Door>().IsEnabled = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            animator.SetBool("IsPressed", true);

            targetDoor.GetComponent<Door>().IsEnabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("IsPressed", false);

        targetDoor.GetComponent<Door>().IsEnabled = false;
    }
}
