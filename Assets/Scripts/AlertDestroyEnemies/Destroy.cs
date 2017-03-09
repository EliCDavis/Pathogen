using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Scene.Brains;

public class Destroy : MonoBehaviour {
    float thisX;
    float thisY;
    float thisZ;
    float speed = 5f;
    EnemyController controllerReference;
    
	// Use this for initialization
	void Start () {
        controllerReference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (controllerReference.detected == 1)
        {
            this.transform.Translate((controllerReference.lastKnown - this.transform.position) * speed * Time.deltaTime);

        }
		//move toward last konwn player position
	}
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehavior target = other.gameObject.GetComponent<PlayerBehavior>();
        if (target != null)
        {
            //damage player
            target.Damage(10, Pathogen.Player.DamageType.Destroyer);
            Destroy(this.gameObject);
        }

    }
}
