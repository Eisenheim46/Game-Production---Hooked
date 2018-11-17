using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDebugger : MonoBehaviour {

    private Text debugger;

    private void Awake()
    {
        PlayerInput.onTriggerUp += TriggerUp;
    }

    private void Start()
    {
        debugger = GetComponent<Text>();
    }

    private void Update()
    {
        
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {

            debugger.text = "Shooting";
        }
    }

    private void TriggerDown()
    {
        debugger.text = "Shooting";
    }

    private void TriggerUp()
    {
        debugger.text = "Released";
    }
}
