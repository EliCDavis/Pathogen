using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBrain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.childCount == 0)
        {
            Debug.Log("You did tha thing");
        }
	}
}
