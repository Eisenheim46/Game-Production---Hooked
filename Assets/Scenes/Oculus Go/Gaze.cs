using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour {

    public void OnGazeEnter()
    {
        Debug.Log("Looking at box");
    }

    public void OnGazeExit()
    {
        Debug.Log("NEIN!!!!!");
    }

}
