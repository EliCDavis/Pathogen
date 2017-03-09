using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathogen.Scene.Veins {

	public class ScoreManager {

		private static ScoreManager instance = null;
		public static ScoreManager GetInstance() {
			if (instance == null) {
				instance = new ScoreManager ();
			}
			return instance;
		}

		int topScore;

		int veinsStagedScore;
		int brainStagedScore;

		private ScoreManager(){
			topScore = 0;
			veinsStagedScore = 0;
			brainStagedScore = 0;
            topScore = PlayerPrefs.GetInt("High Score");
        }

		public void ClearStagedScores() {
			veinsStagedScore = 0;
			brainStagedScore = 0;
		}

		public void ClearVeinsStagedScore() {
			veinsStagedScore = 0;
		}

		public void ClearBrainsStagedScore() {
			brainStagedScore = 0;
		}

		public int GetVeinsStagedScore() {
			return veinsStagedScore;
		}

		public int GetBrainsStagedScore() {
			return brainStagedScore;
		}

		public int GetStagedScore() {
			return brainStagedScore + veinsStagedScore;
		}

		public void AddToVeinsScore(int amount) {
			veinsStagedScore += Mathf.Abs (amount);
		}

		public void AddToBrainsScore(int amount) {
			brainStagedScore += Mathf.Abs (amount);
		}

		private void CommitScore() {
			topScore = Mathf.Max (topScore, GetStagedScore());
            PlayerPrefs.SetInt("High Score", topScore);
            PlayerPrefs.Save();
        }
	
	}

}