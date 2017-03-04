using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour{

    EnemyController reference;


	// Use this for initialization
	void Start () {
        //generate position to spawn
        reference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
        reference.lastKnown = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            reference.spawnDestroy();
            reference.lastKnown = other.transform.position;
            reference.detected = 1;
            //spawn destroy
            //update lastKnownPosition
        }
        if (other.gameObject.name == "Bullet")
        {
            Destroy(this);
            //kill this cell
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            reference.lastKnown = other.transform.position;
        }
        //update lastKnownPosition
    }
}
