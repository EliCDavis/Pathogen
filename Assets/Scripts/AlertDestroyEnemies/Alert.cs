using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Scene.Brains;

public class Alert : MonoBehaviour{

    EnemyController controllerReference;
    GameObject graphics;

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
            controllerReference.lastKnown = target.transform.FindChild("Graphics").position;
            controllerReference.detected = 1;
        }

    }

    void OnTriggerStay(Collider other)
    {
        PlayerBehavior target = other.gameObject.GetComponent<PlayerBehavior>();
        if (target != null)
        {
            controllerReference.lastKnown = target.transform.FindChild("Graphics").position;
        }
    }
}
