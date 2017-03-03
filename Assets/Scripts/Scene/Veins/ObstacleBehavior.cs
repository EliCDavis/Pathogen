using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Player;

namespace Pathogen.Scene.Veins {

	public class ObstacleBehavior : MonoBehaviour {

		[SerializeField]
		/// <summary>
		/// The damage delt when the player collides with this object.
		/// </summary>
		private int damageDeltOnCollision = 10;

		[SerializeField]
		/// <summary>
		/// The destroy on collision.
		/// </summary>
		private bool destroyOnCollision = false;

		void OnCollisionEnter(Collision collision){
			PlayerBehavior player = collision.gameObject.GetComponent<PlayerBehavior> ();
			if (player != null) {
				player.Damage (damageDeltOnCollision, DamageType.Collision);
				if (destroyOnCollision) {
					Destroy (gameObject);
				}
			}
		}

	}

}