﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    float thisX;
    float thisY;
    float thisZ;
    float speed = 0.25f;
    EnemyController controllerReference;
    
	// Use this for initialization
	void Start () {
        controllerReference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
        //generate position to spawn
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x - controllerReference.lastKnown.x < 0f)
        {
            thisX = this.transform.position.x + speed;
        }
        if(this.transform.position.x - controllerReference.lastKnown.x > 0f)
        {
            thisX = this.transform.position.x - speed;
        }
        if (this.transform.position.y  - controllerReference.lastKnown.y < 0f)
        {
            thisY = this.transform.position.y + speed;
        }
        if(this.transform.position.y  - controllerReference.lastKnown.y > 0f)
        {
            thisY = this.transform.position.y - speed;
        }
        if (this.transform.position.z - controllerReference.lastKnown.z < 0f)
        {
            thisZ = this.transform.position.z + speed;
        }
        if(this.transform.position.z - controllerReference.lastKnown.z > 0f)
        {
            thisZ = this.transform.position.z - speed;
        }
        this.transform.position = new Vector3(thisX, thisY, thisZ);
		//move toward last konwn player position
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //damage player
            Destroy(this.gameObject);
        }
        if(other.gameObject.name == "Bullet")
        {
            Destroy(this.gameObject);
        }

    }
}
