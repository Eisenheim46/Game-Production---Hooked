using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {

    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int boxAmount;

    private float currentBoxes;

	// Use this for initialization
	void Start ()
    {
        SpawnBox();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {        
        SpawnBox();
	}

    private void SpawnBox()
    {
        currentBoxes = this.gameObject.transform.childCount;

        if (currentBoxes < boxAmount)
        {
            GameObject boxClone;
            boxClone = Instantiate(boxPrefab, transform.position, transform.rotation) as GameObject;
            boxClone.transform.SetParent(this.gameObject.transform, false);
        }
    }
}
