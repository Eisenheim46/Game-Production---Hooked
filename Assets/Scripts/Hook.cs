using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using UnityEngine;

public class Hook : MonoBehaviour {

    [SerializeField] private Transform hookOrigin;
    [SerializeField] private Transform lineOrigin;
    [SerializeField] private Transform playerRig;
    [SerializeField] private Transform playerCamera;
    [SerializeField]private SteamVR_TrackedObject controller;
    private SteamVR_Controller.Device controllerButtons { get { return SteamVR_Controller.Input((int)controller.index); } }

    public bool GripPressed { get; private set; }

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
    private PlayerPhysics playerPhysics;

    private float currentHookDistance;

    private bool hooked;
    private bool returning;


    // Use this for initialization
    void Start ()
    {
        ropeLine = GetComponent<LineRenderer>();
        hookRb = GetComponent<Rigidbody>();
        playerPhysics = playerRig.GetComponent<PlayerPhysics>();
        playerRb = playerRig.GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        hookParent = transform.parent;

        ropeLine.enabled = false;

        hooked = false;
        returning = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (controllerButtons.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            ShootHook();
        }

        if (hooked)
        {
            if (controllerButtons.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                GripPressed = true;

                ReelTowardsHook();
            }
            else if (controllerButtons.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                GripPressed = false;

                playerRb.useGravity = true;
                playerRb.isKinematic = false;

                //Vector3 direction = target - playerRb.position;

                //direction = Vector3.Normalize(direction);

                //playerRb.velocity = direction * (playerSpeed * velocitySpeed) * Time.deltaTime;
            }
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
        }
        else
        {
            hooked = false;

            returning = true;
        }
            
    }

    private void ReelTowardsHook ()
    {
        transform.parent = null;

        hookRb.isKinematic = true;

        if (playerRb.isKinematic == true)
        {
            playerRb.isKinematic = false;
        }

        if (playerRb.useGravity == true)
        {
            playerRb.useGravity = false;
        }

        ropeLine.enabled = true;

        if (playerPhysics.OnFloor)
        {
            if (transform.position.y > playerCamera.position.y)
            {
                Vector3 target = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

                //playerRb.position = Vector3.MoveTowards(playerRig.position, target, Time.deltaTime * playerSpeed);
                Vector3 direction = target - playerRb.position;

                if (direction.magnitude <= playerSpeed * Time.deltaTime || direction.magnitude == 0f)
                {
                    playerRb.velocity = Vector3.zero;
                }
                else
                {
                    direction = Vector3.Normalize(direction);

                    playerRb.velocity = direction * (playerSpeed * velocityMultiplier) * Time.deltaTime;
                }

            }
            else
            {
                Vector3 target = new Vector3(transform.position.x, playerRb.position.y, transform.position.z);

                //playerRb.position = Vector3.MoveTowards(playerRb.position, target, Time.deltaTime * playerSpeed);
                Vector3 direction = target - playerRb.position;

                if (direction.magnitude <= playerSpeed * Time.deltaTime || direction.magnitude == 0f)
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
        else if (!playerPhysics.OnFloor)
        {
            Vector3 target = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

            // playerRb.position = Vector3.MoveTowards(playerRig.position, target, Time.deltaTime * playerSpeed);

            Vector3 direction = target - playerRb.position;

            if (direction.magnitude <= playerSpeed * Time.deltaTime || direction.magnitude == 0f)
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
        hookRb.velocity = new Vector3(0, 0, 0);

        hooked = false;

        hookRb.isKinematic = true;

        if (!playerPhysics.OnFloor && playerRb.useGravity == false)
        {
            playerRb.useGravity = true;
            playerRb.isKinematic = false;
        }

        this.transform.position = Vector3.MoveTowards(transform.position, hookOrigin.position, Time.deltaTime * hookSpeed);

        currentHookDistance = Vector3.Distance(hookOrigin.position, transform.position);

        if (currentHookDistance <= 0.1)
        {
            if (retractedObject != null)
            {
                retractedObject.parent = null;
                retractedObject.GetComponent<Rigidbody>().isKinematic = false;
            }

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

        else if (collision.gameObject.tag == "Wooden")
        {
            retractedObject = collision.transform;

            retractedObject.parent = this.transform;

            retractedObject.GetComponent<Rigidbody>().isKinematic = true;

            returning = true;
        }

        else
        {
            returning = true;
        }
    }
}
