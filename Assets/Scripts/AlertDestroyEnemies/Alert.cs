using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour{

    EnemyController reference;
    float spawnX;
    float spawnY;
    float spawnZ;


	// Use this for initialization
	void Start () {
        //generate position to spawn
        reference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
        spawnX = Random.Range(-1500f, 1500f);
        spawnY = Random.Range(5f, 1000f);
        spawnZ = Random.Range(-1500f, 1500f);
        this.transform.position = new Vector3(spawnX, spawnY, spawnZ);
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
