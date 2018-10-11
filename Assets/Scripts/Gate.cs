using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsEnabled))]
public class Gate : MonoBehaviour
{

    private Animator animator;

    void Start ()
    {
        GetComponent<IsEnabled>().isEnabled = false;

        animator = GetComponent<Animator>();
        animator.SetBool("IsOpen", false);

    }

    private void Update()
    {
        if (GetComponent<IsEnabled>().isEnabled)
        {
            animator.SetBool("IsOpen", true);
        }
        else
        {
            animator.SetBool("IsOpen", false);
        }
    }
}
