using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookOrigin : MonoBehaviour
{
    [SerializeField] private GameObject target;


    [HideInInspector] public bool IsWooden;
    private RaycastHit hit;

	void Start ()
    {
		
	}

	void Update ()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            target.transform.position = hit.point;
            target.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            target.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (hit.transform != null && (hit.transform.gameObject.tag == "Hookable" || hit.transform.gameObject.tag == "Wooden"))
        {
            IsWooden = true;
        }
        else if (hit.transform != null)
        {
            IsWooden = false;
        }
    }
}
