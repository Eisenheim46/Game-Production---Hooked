using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSounds : MonoBehaviour {

    private AudioSource audio;

	// Use this for initialization
	void Start ()
    {
        audio = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hook")
        {
            audio.Play(0);
        }
    }
}
