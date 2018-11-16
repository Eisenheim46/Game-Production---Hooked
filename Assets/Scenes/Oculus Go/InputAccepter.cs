using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAccepter : MonoBehaviour {

    private void Awake()
    {
        PlayerInput.onTriggerDown += TriggerDown;
        PlayerInput.onTriggerDown += TriggerUp;
    }


private void TriggerDown()
    {

    }

    private void TriggerUp()
    {

    }
}
