using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Scene.Brains;

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
        PlayerBehavior target = other.gameObject.GetComponent<PlayerBehavior>();
        if (target != null)
        {
            controllerReference.spawnDestroy();
            controllerReference.lastKnown = other.transform.position;
            controllerReference.detected = 1;
        }

    }

    void OnTriggerStay(Collider other)
    {
        PlayerBehavior target = other.gameObject.GetComponent<PlayerBehavior>();
        if (target != null)
        {
            controllerReference.lastKnown = other.transform.position;
        }
    }
}
