using UnityEngine;

namespace Pathogen.Scene.Veins {

	/// <summary>
	/// Attatch this to anything you want to be destroyed
	/// </summary>
	public class DestroyableBehavior : MonoBehaviour {

		[SerializeField]
		private int currentHealth;

		[SerializeField]
		private GameObject deathEffect;

		[SerializeField]
		private float deathEffectDuration;

		public void Damage(int amount) {

			// No Hax; Only pain~
			currentHealth -= Mathf.Abs (amount);

			// Die
			if (currentHealth <= 0) {

				if (deathEffect != null && deathEffectDuration > 0) {
					GameObject effect = Instantiate (deathEffect, transform.position, deathEffect.transform.rotation);
					Destroy (effect, deathEffectDuration);
				}

				Destroy (gameObject);

			}

		}

	}

}