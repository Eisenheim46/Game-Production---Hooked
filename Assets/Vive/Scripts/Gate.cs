using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsEnabled))]
public class Gate : MonoBehaviour
{
    [SerializeField] private Transform gateUp;
    [SerializeField] private Transform gateDown;
    [SerializeField] private float speedUp = 1;
    [SerializeField] private float speedDown = 1;
    [SerializeField] private bool startsOpen = false;

    private void Start()
    {
        if (startsOpen)
            transform.position = gateUp.position;
        else
            transform.position = gateDown.position;
    }

    private void Update()
    {
        if (GetComponent<IsEnabled>().isEnabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, gateUp.position, speedUp/100);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, gateDown.position, speedDown/100);
        }

    }
}
