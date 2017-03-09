using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour{

    EnemyController controllerReference;

	// Use this for initialization
	void Start () {
        controllerReference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            controllerReference.spawnDestroy();
            controllerReference.lastKnown = other.transform.position;
            controllerReference.detected = 1;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            controllerReference.lastKnown = other.transform.position;
        }
    }
}
