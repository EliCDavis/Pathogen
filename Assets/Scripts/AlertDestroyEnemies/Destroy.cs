using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Scene.Brains;

public class Destroy : MonoBehaviour {
  
    float speed = 13f;
    EnemyController controllerReference;
    
	// Use this for initialization
	void Start () {
        controllerReference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (controllerReference.detected == 1)
        {
			Vector3 movementDirection = (controllerReference.lastKnown - transform.position);

			if (movementDirection.x != movementDirection.x || float.IsInfinity(movementDirection.x)) {
				return;
			}

			if (movementDirection.y != movementDirection.y || float.IsInfinity(movementDirection.y)) {
				return;
			}

			if (movementDirection.z != movementDirection.z || float.IsInfinity(movementDirection.z)) {
				return;
			}

			if (transform != null) {
				transform.Translate (movementDirection * speed * Time.deltaTime);
			}
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
            Destroy(gameObject);
        }

    }
}
