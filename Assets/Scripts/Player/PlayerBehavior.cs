using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathogen.Player {

	public class PlayerBehavior : MonoBehaviour {

		private float playerSpeed;
		private float rotateSpeed;

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

		/// <summary>
		/// The graphics used to display the character.
		/// </summary>
		[SerializeField]
		private GameObject graphics;

		private int health;

		[SerializeField]
		private Vector2 veinCenter;

		[SerializeField]
		private float maxDistanceFromVeinCenter;

		// Use this for initialization
		void Start () {
			this.playerSpeed = 10f;
			this.rotateSpeed = 90f;
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
		public void Damage(int damage){
			health = Mathf.Max (health - Mathf.Abs(damage), 0);
			if (health == 0) {
				Die ("You took too much damage!");
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
		/// Die the specified reason.
		/// </summary>
		/// <param name="reason">Reason the player died.</param>
		private void Die(string reason){
			Destroy (transform.gameObject);
		}

	}

}