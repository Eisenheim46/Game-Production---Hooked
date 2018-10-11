using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetScript : MonoBehaviour {


    [SerializeField] private Transform target;
    [SerializeField] private Transform yTarget;


	// Update is called once per frame
	private void Update ()
    {
        Vector3 newLocation = target.position;

        newLocation.y = yTarget.position.y;


        this.transform.position = newLocation;
	}
}
