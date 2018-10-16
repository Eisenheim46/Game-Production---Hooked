using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookOrigin : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private bool isWooden;
    private RaycastHit hit;

    public bool IsWooden { get { return isWooden; } }

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
            isWooden = true;
        }
        else if (hit.transform != null)
        {
            isWooden = false;
        }
    }
}
