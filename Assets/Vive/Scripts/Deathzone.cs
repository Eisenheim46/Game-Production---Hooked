using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = checkpoint.position;

            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        else if (other.gameObject.tag == "Wood")
        {
            other.transform.position = other.GetComponent<BoxRespawn>().OriginPosition.position;
        }
    }
}
