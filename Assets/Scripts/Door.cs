using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] private bool isOpen;

    public bool IsOpen { get { return isOpen; } set { isOpen = value; } }
    public bool IsEnabled { get; set; }

    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        IsEnabled = false;

        animator = GetComponent<Animator>();

        animator.SetBool("IsOpen", isOpen);
    }

    private void Update()
    {
        if (IsEnabled)
        {
            animator.SetBool("IsOpen", true);
        }
        else
        {
            animator.SetBool("IsOpen", false);
        }
    }
}
