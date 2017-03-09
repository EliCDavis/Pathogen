using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Scene.Brains;

public class Destroy : MonoBehaviour {
  
    float speed = 150f;
    EnemyController controllerReference;
    
	// Use this for initialization
	void Start () {
        controllerReference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (controllerReference.detected == 1)
        {
			Vector3 movementDirection = (controllerReference.lastKnown - transform.position).normalized;
			transform.Translate (movementDirection * speed * Time.deltaTime);
        }
		//move toward last konwn player position
	}

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehavior target = other.gameObject.GetComponent<PlayerBehavior>();
        if (target != null)
        {
            target.Damage(5, Pathogen.Player.DamageType.Destroyer);
            Destroy(gameObject);
        }

    }
}
