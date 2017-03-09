using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Scene.Brains;

public class BrainPart : MonoBehaviour {
    public Pathogen.Scene.Veins.DestroyableBehavior healthReference;
    public EnemyController enemyReference;
    int spawn;
	// Use this for initialization
	void Start () {
        healthReference = this.GetComponent<Pathogen.Scene.Veins.DestroyableBehavior>();
        enemyReference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (healthReference.getHealth() < 400 && healthReference.getHealth() > 300)
        {
            spawn = 0;
        }
        if (healthReference.getHealth() == 400 && spawn == 0)
        {
            for(int i = 0; i < 5; i++)
            {
                enemyReference.spawnAlert();
            }
            spawn = 1;
        }
        if (healthReference.getHealth() < 300 && healthReference.getHealth() > 200)
        {
            spawn = 0;
        }
        if (healthReference.getHealth() == 300 && spawn == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                enemyReference.spawnAlert();
            }
            spawn = 1;
        }
        if (healthReference.getHealth() < 200 && healthReference.getHealth() > 100)
        {
            spawn = 0;
        }
        if (healthReference.getHealth() == 200 && spawn == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                enemyReference.spawnAlert();
            }
            spawn = 1;
        }
        if (healthReference.getHealth() == 100 && spawn == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                enemyReference.spawnAlert();
            }
            spawn = 1;
        }
    }
}
