 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenDoorKeyhole : MonoBehaviour
{
    [SerializeField] private GameObject woodenDoor;
    [SerializeField] private bool startsLocked;

    private bool isUnlocked;

	void Start ()
    {
        if (startsLocked)
            isUnlocked = false;
        LockUnlock();

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            other.GetComponent<Pickable>().enabled = false;
            isUnlocked = true;
            LockUnlock();
        }
    }

    private void LockUnlock()
    {
        if (isUnlocked)
            woodenDoor.GetComponent<Rigidbody>().isKinematic = false;
        else
            woodenDoor.GetComponent<Rigidbody>().isKinematic = true;
    }
}
