using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using UnityEngine;

public class HookOculus : MonoBehaviour {

    [SerializeField] private Transform hookOrigin;
    [SerializeField] private Transform lineOrigin;
    [SerializeField] private Transform playerRig;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private OculusPlayerPhysics playerPhysics;
    

    [Header("Manual Input")]
    [SerializeField] private float hookSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float velocityMultiplier;
    [SerializeField] private float maxHookDistance;
    [SerializeField] private float maxReturnDistance;

    [Header("Audio")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip pullClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AnimationCurve Curve;

    private LineRenderer ropeLine;
    private Rigidbody hookRb;
    private Rigidbody playerRb;
    private Transform hookParent;
    private Transform retractedObject;
    private AudioSource audio;


    private float currentHookDistance;

    private bool shooting = false;
    private bool hooked = false;
    private bool returning = false;



    // Use this for initialization
    void Start ()
    {
        ropeLine = GetComponent<LineRenderer>();
        hookRb = GetComponent<Rigidbody>();
        playerRb = playerRig.GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        hookParent = transform.parent;

        ropeLine.enabled = false;

        hooked = false;
        returning = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            ShootHook();

            playerPhysics.TriggerPressed = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            returning = true;

            shooting = false;

            playerPhysics.TriggerPressed = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {

            if (retractedObject != null)
            {
                retractedObject.parent = null;
                retractedObject.GetComponent<Rigidbody>().isKinematic = false;

                retractedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTrackedRemote) * 50f;
                retractedObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTrackedRemote) * .5f;
            }

            returning = true;
        }

        if (hooked)
        {
            ReelTowardsHook();
        }

        if (returning)
        {
            ReturnHook();
        }

        currentHookDistance = Vector3.Distance(hookOrigin.position, transform.position);

        if (currentHookDistance >= maxHookDistance)
        {
            returning = true;
        }

        RenderLine();

    }

    private void ShootHook ()
    {
        if (returning == false && hooked == false)

        {
            transform.parent = null;

            hookRb.isKinematic = false;

            ropeLine.enabled = true;

            hookRb.velocity = transform.TransformDirection(0, hookSpeed, 0);

            shooting = true;
        }
        //else
        //{
        //    hooked = false;


        //    returning = true;
        //}
            
    }

    private void ReelTowardsHook ()
    {
        transform.parent = null;

        hookRb.isKinematic = true;

        //if (playerRb.isKinematic == true)
        //{
        //    playerRb.isKinematic = false;
        //}

        //if (playerRb.useGravity == true)
        //{
        //    playerRb.useGravity = false;
        //}

        ropeLine.enabled = true;

        if (playerPhysics.OnFloor)
        {
            Vector3 direction;
            float magnitude;

            if (transform.position.y > playerCamera.position.y)
            {
                Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z);

               // Vector3 offeset = playerCamera.position - playerRig.position;

                //target = target - offeset;

                direction = target - playerRb.position;
                magnitude = direction.magnitude;
            }
            else
            {
                Vector3 target = new Vector3(transform.position.x, playerRb.position.y, transform.position.z);

                direction = target - playerRb.position;
                magnitude = direction.magnitude;
            }


            if (magnitude <= playerSpeed * Time.deltaTime || magnitude == 0f)
            {
                playerRb.velocity = Vector3.zero;
            }
            else
            {
                direction = Vector3.Normalize(direction);

                playerRb.velocity = direction * (playerSpeed * velocityMultiplier) * Time.deltaTime;
            }
        }
        else if (!playerPhysics.OnFloor)
        {
            Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z);

           // Vector3 offeset = playerCamera.position - playerRig.position;

            //target = target - offeset;

            Vector3 direction = target - playerRb.position;
            float magnitude = direction.magnitude;

            if (magnitude <= playerSpeed * Time.deltaTime || magnitude == 0f)
            {
                playerRb.velocity = Vector3.zero;
            }
            else
            {
                direction = Vector3.Normalize(direction);

                playerRb.velocity = direction * (playerSpeed * velocityMultiplier) * Time.deltaTime;
            }

        }
    }

    private void ReturnHook()
    {
 //       hookRb.velocity = new Vector3(0, 0, 0);

        hooked = false;

        hookRb.isKinematic = true;


        this.transform.position = Vector3.MoveTowards(transform.position, hookOrigin.position, Time.deltaTime * hookSpeed);

        currentHookDistance = Vector3.Distance(hookOrigin.position, transform.position);

        if (currentHookDistance <= 0.1)
        {

            ropeLine.enabled = false;

            transform.parent = hookParent;

            transform.rotation = hookOrigin.rotation;

            transform.position = hookOrigin.position;

            returning = false;
        }
    }


    private void RenderLine()
    {
        //Set Positions of the line Renderer
        ropeLine.SetPosition(1, lineOrigin.position);
        ropeLine.SetPosition(0, this.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hookable")
        {
            hookRb.velocity = new Vector3 (0,0,0);
            hooked = true;
        }
        else if (collision.gameObject.tag == "Wood" && shooting)
        {
            if (collision.transform.parent != this.transform)
            {
                retractedObject = collision.transform;

                retractedObject.parent = this.transform;

                retractedObject.rotation = this.transform.rotation;

                retractedObject.GetComponent<Rigidbody>().isKinematic = true;

                returning = true;
            }
        }
        else
        {
            returning = true;
        }
    }
}
