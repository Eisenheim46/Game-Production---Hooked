using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSounds : MonoBehaviour
{

    [SerializeField] private AudioClip woodClip;
    [SerializeField] private AudioClip metalClip;
    [SerializeField] private AudioClip doorClip;
    [SerializeField] private AudioClip gateClip;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wood")
        {
            audio.clip = woodClip;
            audio.PlayDelayed(0);
        }
        if (collision.gameObject.tag == "Metal")
        {
            audio.clip = metalClip;
            audio.PlayDelayed(0);
        }
        if (collision.gameObject.tag == "Door")
        {
            audio.clip = doorClip;
            audio.PlayDelayed(0);
        }
        if (collision.gameObject.tag == "Gate")
        {
            audio.clip = gateClip;
            audio.PlayDelayed(0);
        }
    }
}
