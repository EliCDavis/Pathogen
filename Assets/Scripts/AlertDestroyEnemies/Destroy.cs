using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    float thisX;
    float thisY;
    float thisZ;
    float speed = 0.15f;
    EnemyController controllerReference;
    float spawnX;
    float spawnY;
    float spawnZ;
    //PlayerBehavior playerReference;
    
	// Use this for initialization
	void Start () {
        controllerReference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
        //playerReference = GameObject.Find("PlayerBehavior").GetComponent<PlayerBehavior>();
        spawnX = Random.Range(-1500f, 1500f);
        spawnY = Random.Range(5f, 1000f);
        spawnZ = Random.Range(-1500f, 1500f);
        this.transform.position = new Vector3(spawnX, spawnY, spawnZ);
    }
	
	// Update is called once per frame
	void Update () {
        if (controllerReference.detected == 1)
        {


            if (this.transform.position.x - controllerReference.lastKnown.x < 0f)
            {
                thisX = this.transform.position.x + speed;
            }
            if (this.transform.position.x - controllerReference.lastKnown.x > 0f)
            {
                thisX = this.transform.position.x - speed;
            }
            if (this.transform.position.y - controllerReference.lastKnown.y < 0f)
            {
                thisY = this.transform.position.y + speed;
            }
            if (this.transform.position.y - controllerReference.lastKnown.y > 0f)
            {
                thisY = this.transform.position.y - speed;
            }
            if (this.transform.position.z - controllerReference.lastKnown.z < 0f)
            {
                thisZ = this.transform.position.z + speed;
            }
            if (this.transform.position.z - controllerReference.lastKnown.z > 0f)
            {
                thisZ = this.transform.position.z - speed;
            }
            this.transform.position = new Vector3(thisX, thisY, thisZ);
        }
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
