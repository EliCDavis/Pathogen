﻿using UnityEngine;
using Pathogen.Scene.Veins;

namespace Pathogen.Player {

	public class ProjectileBehavior : MonoBehaviour {

		private int damage = 10;

		void Start(){
			Destroy (gameObject, 10f);
		}

		void OnCollisionEnter(Collision collision){
			DestroyableBehavior target = collision.gameObject.GetComponent<DestroyableBehavior> ();
			if (target != null) {
				target.Damage (damage);
				Destroy (gameObject);
			}
		}
	}

}