using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathogen.Scene.Veins {

	enum GameState {
		NotBegan,
		Started,
		Paused,
		Ended
	}

	public class GameBehavior : MonoBehaviour {

		/// <summary>
		/// The current state of the game.
		/// </summary>
		private GameState currentState = GameState.NotBegan;

		[SerializeField]
		private AudioSource heartBeatClip;

		List<GameObject> veinsSpawned;

		GameObject playerInstance;

		void Start() {
			veinsSpawned = new List<GameObject> ();
			StartGame ();
			StartCoroutine (AnimateHeartbeat ());
		}

		private void StartGame() {
			currentState = GameState.Started;
			playerInstance = LoadPlayer ();
			playerInstance.transform.position = Vector3.zero;
			StartCoroutine (SpawnVeinSections ());
		}

		private GameObject LoadPlayer() {
			return Instantiate(Resources.Load <GameObject>("Rails/Player"), Vector3.zero, Quaternion.identity);
		}


		/// <summary>
		/// Take care of spawning veins in a seperate thread.
		/// </summary>
		/// <returns>The vein sections.</returns>
		private IEnumerator SpawnVeinSections(){
			while (currentState != GameState.Ended && playerInstance != null) {

				while(playerInstance.transform.position.z > (veinsSpawned.Count-10) * 27.0f){
					veinsSpawned.Add(VeinFactory.CreateVein (new Vector3(0, -1, veinsSpawned.Count*27)));
				}

				yield return new WaitForSeconds(.5f);

			}
		}


		private IEnumerator AnimateHeartbeat(){
			while (heartBeatClip != null) {
				yield return new WaitForSeconds(.1f);
				if (!heartBeatClip.isPlaying && Mathf.Max (0, (Mathf.Sin (Time.time * 4) * 2) - (Mathf.Pow (Mathf.Sin (Time.time * 4), 4) / .8f)) > 1) {
					heartBeatClip.Play ();
				}
			}
		}

	}

}