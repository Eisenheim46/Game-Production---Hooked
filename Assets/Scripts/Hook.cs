using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

    [SerializeField] private Transform hookOrigin;
    [SerializeField] private Transform lineOrigin;
    [SerializeField] private Transform player;
    [SerializeField] private SteamVR_TrackedController controller;

    [Header("Manual Input")]
    [SerializeField] private float hookSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxHookDistance;
    [SerializeField] private float maxReturnDistance;

    [Header("Audio")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip pullClip;
    [SerializeField] private AudioClip hitClip;

    private LineRenderer ropeLine;
    private Rigidbody hookRb;
    private Rigidbody playerRb;
    private Transform hookParent;
    private Transform retractedObject;
    private AudioSource audio;
    

    private float currentHookDistance;

    private bool fired;
    private bool hooked;
    private bool returning;
    private bool falling;

    private void OnEnable()
    {
        controller.TriggerClicked += ShootHook;
    }

    private void OnDisable()
    {
        controller.TriggerClicked -= ShootHook;
    }


    // Use this for initialization
    void Start ()
    {
        ropeLine = GetComponent<LineRenderer>();
        hookRb = GetComponent<Rigidbody>();
        playerRb = player.GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        hookParent = transform.parent;

        ropeLine.enabled = false;

        fired = false;
        hooked = false;
        returning = false;
        falling = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (fired == true && hooked == false)
        {
            transform.parent = null;
            hookRb.isKinematic = false;

            ropeLine.enabled = true;

            hookRb.velocity = transform.TransformDirection(0, hookSpeed, 0);

            currentHookDistance = Vector3.Distance(hookOrigin.position, transform.position);

            if (currentHookDistance >= maxHookDistance)
            {
                returning = true;
            }
        }

        if (controller.gripped)
        {
            if (hooked)
            {
                playerRb.isKinematic = false;
                ReelTowardsHook();
            }
        }


        if (returning)
        {
            ReturnHook();
        }

        if (falling)
        {
            playerRb.isKinematic = false;
            //player.position = Vector3.MoveTowards(player.position, new Vector3(player.position.x, 0, player.position.z), Time.deltaTime * (playerSpeed/2));

            if (player.position.y == 0)
            {
                playerRb.isKinematic = true;
                falling = false;
            }
        }

        RenderLine();
        
    }

    private void ShootHook (object sender, ClickedEventArgs e)
    { 

        if(returning == false)
        {
            fired = !fired;
            Debug.Log(fired);
        }


        if (fired == false)
            returning = true;
    }

    private void ReelTowardsHook ()
    {
        transform.parent = null;
        hookRb.isKinematic = false;

        ropeLine.enabled = true;

        player.position = Vector3.MoveTowards(player.position, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Time.deltaTime * playerSpeed);
    }

    private void ReturnHook()
    {
        hookRb.velocity = new Vector3(0, 0, 0);

        hookRb.isKinematic = true;

        this.transform.position = Vector3.MoveTowards(transform.position, hookOrigin.position, Time.deltaTime * hookSpeed);

        currentHookDistance = Vector3.Distance(hookOrigin.position, transform.position);

        if (currentHookDistance <= 0.1)
        {
            if (retractedObject != null)
            {
                retractedObject.parent = null;
                retractedObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            
            if (hooked)
            {
                falling = true;
            }

            ropeLine.enabled = false;

            transform.parent = hookParent;

            transform.rotation = hookOrigin.rotation;

            transform.position = hookOrigin.position;

            returning = false;
            fired = false;
            hooked = false;
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
