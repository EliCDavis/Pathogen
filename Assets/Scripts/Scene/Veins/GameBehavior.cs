﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathogen.Player;

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

		private float redCellSpawnPeriod = 0.05f;

		/// <summary>
		/// How much time the game has progressed
		/// </summary>
		private float timeElapsed = 0f;

		private float timeNeededToComplete = 60f;

		[SerializeField]
		private GameObject winMenu;

		[SerializeField]
		private Text highschore;

		[SerializeField]
		private GameObject pauseMenu;

		[SerializeField]
		private GameObject deathMenu;

		[SerializeField]
		private GameObject howTo;

		[SerializeField]
		private Text deathText;

		private List<GameObject> redCellsInScene;

		List<GameObject> veinsSpawned;

		GameObject playerInstance;

		void Start() {
			veinsSpawned = new List<GameObject> ();
			StartCoroutine (AnimateHeartbeat ());
			CloseAllMenus ();
			howTo.SetActive (true);
			playerInstance = LoadPlayer ();
			StartCoroutine (SpawnVeinSections ());
		}

		void Update(){
			if(Input.GetKeyUp (KeyCode.P) && CanTogglePause()){
				TogglePause ();
			}

			if(currentState == GameState.Started && !CurrentlyPaused()){
				timeElapsed += Time.deltaTime;
				Debug.Log (timeElapsed);
			}

			if(currentState == GameState.Started && timeElapsed >= timeNeededToComplete){
				PlayerWins ();
			}
		}

		/// <summary>
		/// Determines whether the player can toggle pause.
		/// </summary>
		/// <returns><c>true</c> if this instance can toggle pause; otherwise, <c>false</c>.</returns>
		private bool CanTogglePause(){
			return currentState == GameState.Paused || currentState == GameState.Started;
		}

		private void PlayerWins() {
			currentState = GameState.Ended;
			playerInstance.GetComponent<PlayerBehavior>().DetachCamera ();
			Destroy (playerInstance);
			highschore.text = "Score: " + ScoreManager.GetInstance ().GetVeinsStagedScore ();
			winMenu.SetActive (true);
		}

		/// <summary>
		/// Opens and closes the pause menu as well as
		/// </summary>
		private void TogglePause() {
			if (CurrentlyPaused()) {
				pauseMenu.SetActive (false);
				Time.timeScale = 1;
			} else {
				pauseMenu.SetActive (true);
				Time.timeScale = 0;
			}
		}

		// Whether or not we're currently paused
		private bool CurrentlyPaused() {
			return pauseMenu.activeSelf;
		}

		public void StartGame() {
			redCellsInScene = new List<GameObject> ();
			currentState = GameState.Started;
			StartCoroutine (SpawnBloodCells());
			howTo.SetActive (false);
			ScoreManager.GetInstance ().ClearStagedScores ();
			timeElapsed = 0;
		}

		public void PlayerDied(string reason){
			currentState = GameState.Ended;
			CloseAllMenus ();
			deathMenu.SetActive (true);
			deathText.text = reason;
		}

		private GameObject LoadPlayer() {
			GameObject player = Instantiate(Resources.Load <GameObject>("Rails/Player"), Vector3.zero, Quaternion.identity);
			player.GetComponent<PlayerBehavior> ().SetGameBehavior(this);
			return player;
		}

		private void CloseAllMenus() {
			pauseMenu.SetActive (false);
			deathMenu.SetActive (false);
			howTo.SetActive (false);
			winMenu.SetActive (false);
		}


		/// <summary>
		/// Take care of spawning veins in a seperate thread.
		/// </summary>
		/// <returns>The vein sections.</returns>
		private IEnumerator SpawnVeinSections(){
			while (currentState != GameState.Ended && playerInstance != null) {

				while(playerInstance.transform.position.z > (veinsSpawned.Count-10) * 27.0f){

					if (Random.Range (0, 1f) > .8f && currentState == GameState.Started) {
						veinsSpawned.Add(VeinFactory.CreateVein (new Vector3(0, -1, veinsSpawned.Count*27), Random.Range(-90, 0)));
					} else {
						veinsSpawned.Add(VeinFactory.CreateVein (new Vector3(0, -1, veinsSpawned.Count*27)));
					}

				}

				yield return new WaitForSeconds(.5f);

			}
		
		}


		/// <summary>
		/// Animates the heartbeat in a seperate thread
		/// </summary>
		/// <returns>The heartbeat.</returns>
		private IEnumerator AnimateHeartbeat(){
			while (heartBeatClip != null) {
				yield return new WaitForSeconds(.1f);
				if (!heartBeatClip.isPlaying && Mathf.Max (0, (Mathf.Sin (Time.time * 4) * 2) - (Mathf.Pow (Mathf.Sin (Time.time * 4), 4) / .8f)) > 1) {
					heartBeatClip.Play ();
				}
			}
		}

		private IEnumerator SpawnBloodCells(){
			while (currentState != GameState.Ended && playerInstance != null) {

				// Spawn a cell
				GameObject cell = CellFactory.CreateRedBloodCell (new Vector3(Random.Range(-9, 9), Random.Range(-10, 8), veinsSpawned.Count*27));
				cell.GetComponent<Rigidbody> ().AddForce (Vector3.back*20, ForceMode.Impulse);
				redCellsInScene.Add (cell);

				// Clean up cells the player has passed or destroyed
				for (var i = redCellsInScene.Count - 1; i >= 0; i--) {
					if (redCellsInScene[i] == null || redCellsInScene[i].transform.position.z + 100 < playerInstance.transform.position.z) {
						Destroy (redCellsInScene[i]);
						redCellsInScene.Remove (redCellsInScene[i]);
					}
				}

				// Wait till next time to spawn
				yield return new WaitForSeconds(redCellSpawnPeriod);

			}
		}

	}

}