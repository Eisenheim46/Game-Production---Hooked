using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour {

    [SerializeField] private string sceneToLoad;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(sceneToLoad);

    }



}
