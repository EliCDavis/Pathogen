﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Cameras;

namespace Pathogen.Player {

	public class PlayerBehavior : MonoBehaviour {

		private float playerSpeed;

		static readonly int MAX_HEALTH = 100;

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

		private int health;

		[SerializeField]
		private Vector2 veinCenter;

		[SerializeField]
		private float maxDistanceFromVeinCenter;

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

		// Use this for initialization
		void Start () {
			this.playerSpeed = 10f;
			this.health = MAX_HEALTH;
		}
		
		// Update is called once per frame
		void Update () {
			transform.Translate (Vector3.forward * playerSpeed * Time.deltaTime);
			InputUpdate ();
		}

		void InputUpdate() {
			turn (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), veinCenter, maxDistanceFromVeinCenter);

			if (Input.GetKeyDown (KeyCode.Space)) {
				Shoot ();
			}
		}

		/// <summary>
		/// Pivots the player towards the direction.
		/// Vector <1,1>: turns player towards top left of screen
		/// </summary>
		/// <param name="direction">Normalized Direction</param>
		/// <param name="tubeCenter">Center of the tube the player is moving through.</param>
		/// <param name="maxOffsetFromCenter">Center of the tube the player is moving through.</param>
		void turn (Vector2 direction, Vector2 tubeCenter, float maxOffsetFromCenter) {

			// Move the player in the proper direction while maintaining proper bounds..
			transform.Translate (new Vector3 (direction.x, direction.y).normalized * Time.deltaTime * playerSpeed);

			float distFromCenter = Vector2.Distance (new Vector2(transform.position.x, transform.position.y), tubeCenter);
			if (distFromCenter > maxOffsetFromCenter) {
				transform.position = transform.position - ((new Vector3(transform.position.x, transform.position.y, 0) - new Vector3 (tubeCenter.x, tubeCenter.y, 0)).normalized * (distFromCenter-maxOffsetFromCenter));
			}
			// Rotate properly to look in the direction we're flying..
			Vector3 lookDirection = transform.forward*2;
			lookDirection += transform.right * direction.x * 2;
			lookDirection += transform.up * direction.y * 2;
			graphics.transform.LookAt (lookDirection + graphics.transform.position);

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
			GameObject bullet = Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
			bullet.GetComponent<Rigidbody> ().AddForce (bulletSpawn.transform.forward*30, ForceMode.Impulse);
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

		private IEnumerator AnimateTakingDamage(int amountOfDamage, CameraMotionBlur blur){
			blur.velocityScale = 34;
			yield return new WaitForSeconds(.5f);
			blur.velocityScale = 0;
		}

	}

}