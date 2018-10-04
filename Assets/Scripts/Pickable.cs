using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
