﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [SerializeField] private int requiredMass;
    [SerializeField] private bool staysActivated;

    [SerializeField] private Material deactivatedMaterial;
    [SerializeField] private Material activatedMaterial;

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
        renderer.material = deactivatedMaterial;
	}
	
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pickable")
        {
            if (collision.rigidbody.mass == requiredMass)
            {
                animator.SetBool("isPressed", true);

                renderer.material = activatedMaterial;

                targetDoor.GetComponent<Door>().IsEnabled = true;
            }   
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (staysActivated == false)
        {
            animator.SetBool("isPressed", false);

            renderer.material = deactivatedMaterial;

            targetDoor.GetComponent<Door>().IsEnabled = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            animator.SetBool("isPressed", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isPressed", false);
    }
}
