using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Scene.Veins;
using UnityEngine.UI;

public class GrabScores : MonoBehaviour {

	public Text score;

	void Start () {
		score.text = "Score: " + ScoreManager.GetInstance ().GetStagedScore ();
	}

}
