using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Player;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Cameras;

namespace Pathogen.Scene.Brains {

	public class PlayerBehavior : MonoBehaviour {

		private float speed = 7.5f;

		private float rotateSpeed = 90f;

		private float maxVelocityChange = 10.0f;

		[SerializeField]
		/// <summary>
		/// The location the bullet will spawn out of.
		/// </summary>
		private Transform bulletSpawn;

		[SerializeField]
		/// <summary>
		/// The bullet that will spawn from the player when they shoot
		/// </summary>
		private GameObject bulletPrefab;

		[SerializeField]
		/// <summary>
		/// The graphics used to display the character.
		/// </summary>
		private GameObject graphics;

		[SerializeField]
		/// <summary>
		/// The camera that is following the player
		/// </summary>
		private GameObject playerCamera;

		[SerializeField]
		/// <summary>
		/// The effect that will play when the player dies
		/// </summary>
		private GameObject deathEffect;

		private int health = 100;

		private Rigidbody rb;

		void Start() {
			rb = gameObject.GetComponent<Rigidbody> ();
		}

		void Update() {

			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(0, Input.GetAxis("Vertical")*speed*.75f, speed);
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rb.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			rb.AddForce(velocityChange, ForceMode.VelocityChange);

			// Rotate appropriatly
			var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + Input.GetAxis("Horizontal")*Time.deltaTime*rotateSpeed, transform.eulerAngles.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * rotateSpeed);

			if (Input.GetKeyDown (KeyCode.Space)) {
				Shoot ();
			}

		}

		/// <summary>
		/// Damage the player by a finite amount.
		/// </summary>
		/// <param name="damage">Amount of damage to hurt the player by</param>
		public void Damage(int damage, DamageType typeOfDamage){

			health = Mathf.Max (health - Mathf.Abs(damage), 0);
			if (health == 0) {

				string deathMessage;

				switch (typeOfDamage) {

				case DamageType.Collision:
					deathMessage = "Stop running into things!";
					break;

				default:
					deathMessage = "You took too much damage!";
					break;

				}

				Die (deathMessage);
			} else {
				StartCoroutine (AnimateTakingDamage (damage, playerCamera.GetComponent<CameraMotionBlur>()));
			}
		}

		/// <summary>
		/// Player shoots a 'bullet' in the direction it's facing
		/// </summary>
		private void Shoot(){
			GameObject bullet = Instantiate (bulletPrefab, bulletSpawn.position, graphics.transform.rotation);
			bullet.GetComponent<Rigidbody> ().velocity = rb.velocity;
			bullet.GetComponent<Rigidbody> ().AddForce (graphics.transform.forward*50, ForceMode.Impulse);
		}

		/// <summary>
		/// Destroys the player object and ends the game.
		/// </summary>
		private void Die(){
			Die ("You died!");
		}

		/// <summary>
		/// Destroys the player object and ends the game.
		/// </summary>
		/// <param name="reason">Reason the player died.</param>
		private void Die(string reason){
			playerCamera.transform.parent = null;
			Destroy (playerCamera.GetComponent<ProtectCameraFromWallClip> ());
			GameObject deathEffectInstance = Instantiate (deathEffect, graphics.transform.position, Quaternion.identity);
			Destroy (deathEffectInstance, .95f);
			Destroy (transform.gameObject);
		}

		/// <summary>
		/// Starts a seperate thread for taking care of shaking
		/// the camera.
		/// </summary>
		/// <returns>IEnumerator.</returns>
		/// <param name="amountOfDamage">Amount of damage.</param>
		/// <param name="blur">Blur object for animation</param>
		private IEnumerator AnimateTakingDamage(int amountOfDamage, CameraMotionBlur blur){
			blur.velocityScale = 34;
			yield return new WaitForSeconds(.5f);
			blur.velocityScale = 0;
		}

	}

}