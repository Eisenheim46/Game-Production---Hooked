using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [SerializeField] private SteamVR_TrackedController controller;

    private bool canGrab;
    private GameObject pickedObject;

   // private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device controllerDevice { get{return SteamVR_Controller.Input((int)controller.controllerIndex); } }

    private void OnEnable()
    {
        //controller = GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += grabObject;
        controller.TriggerUnclicked += dropObject;
    }

    private void OnDisable()
    {
        controller.TriggerClicked -= grabObject;
        controller.TriggerUnclicked -= dropObject;
    }


    private void grabObject(object sender, ClickedEventArgs e)
    {
        if (canGrab && pickedObject != null)
        {
            Debug.Log("Grabbed");
            pickedObject.GetComponent<Rigidbody>().isKinematic = true;
            pickedObject.transform.parent = this.gameObject.transform;
        }
    }

    private void dropObject(object sender, ClickedEventArgs e)
    {
        Transform origin;

        if (pickedObject != null)
        {
            Debug.Log("Dropped");

            origin = controller.transform.parent;

            pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedObject.transform.parent = null;

            pickedObject.GetComponent<Rigidbody>().velocity = origin.TransformVector(controllerDevice.velocity);
            pickedObject.GetComponent<Rigidbody>().angularVelocity = origin.TransformVector(controllerDevice.angularVelocity * 0.25f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wooden" || other.gameObject.tag == "Metal" || other.gameObject.tag == "Key")
        {
            canGrab = true;
            pickedObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wooden" || other.gameObject.tag == "Metal" || other.gameObject.tag == "Key")
        {
            canGrab = false;
        }
    }
}
