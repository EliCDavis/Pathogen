using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathogen.Player {

	public class PlayerBehavior : MonoBehaviour {

		private float playerSpeed;
		private float rotateSpeed;

		static readonly int MAX_HEALTH = 100;

		/// <summary>
		/// The graphics used to display the character.
		/// </summary>
		[SerializeField]
		private GameObject graphics;

		private int health;

		[SerializeField]
		private Rect boundsForPlayerMovement;

		// Use this for initialization
		void Start () {
			this.playerSpeed = 10f;
			this.rotateSpeed = 90f;
			this.health = MAX_HEALTH;
		}
		
		// Update is called once per frame
		void Update () {
			transform.Translate (Vector3.forward * playerSpeed * Time.deltaTime);
			turn (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), boundsForPlayerMovement);
		}


		/// <summary>
		/// Pivots the player towards the direction.
		/// Vector <1,1>: turns player towards top left of screen
		/// </summary>
		/// <param name="vector">Normalized Direction</param>
		/// <param name="bounds">Bounds.</param>
		void turn (Vector2 direction, Rect bounds) {

			// Move the player in the proper direction while maintaining proper bounds..
			transform.Translate (new Vector3 (direction.x * Time.deltaTime * playerSpeed, direction.y * Time.deltaTime * playerSpeed));

			// Rotate properly to look in the direction we're flying..
			Vector3 lookDirection = transform.forward*2;
			lookDirection += transform.right * direction.x * 2;
			lookDirection += transform.up * direction.y * 2;
			graphics.transform.LookAt (lookDirection + graphics.transform.position);


		}

		public void Damage(int damage){

		}

	}

}