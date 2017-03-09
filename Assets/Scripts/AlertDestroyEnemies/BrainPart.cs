using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathogen.Scene.Brains;

public class BrainPart : MonoBehaviour {
    public Pathogen.Scene.Veins.DestroyableBehavior healthReference;
    public EnemyController enemyReference;

	[SerializeField]
	public AudioSource soundEffect;

    int spawn;
	// Use this for initialization
	void Start () {
        healthReference = this.GetComponent<Pathogen.Scene.Veins.DestroyableBehavior>();
        enemyReference = GameObject.Find("EnemyController").GetComponent<EnemyController>();
    }

	int lastHealth = 500;

	private void DamageAnimation() {
		transform.localScale = transform.localScale * .98f;
		StartCoroutine (AnimateTakingDamage());
		soundEffect.Play ();

	}

	private IEnumerator AnimateTakingDamage(){
		gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
		yield return new WaitForSeconds(.1f);
		gameObject.GetComponent<MeshRenderer> ().material.color = Color.white;
	}

	// Update is called once per frame
	void Update () {

		if(lastHealth != healthReference.getHealth()){
			lastHealth = healthReference.getHealth ();
			enemyReference.BrainPartHit ();
			DamageAnimation ();
		}

        if (healthReference.getHealth() < 400 && healthReference.getHealth() > 300)
        {
            spawn = 0;
        }
        if (healthReference.getHealth() == 400 && spawn == 0)
        {
            for(int i = 0; i < 8; i++)
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
            for (int i = 0; i < 8; i++)
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
            for (int i = 0; i < 8; i++)
            {
                enemyReference.spawnAlert();
            }
            spawn = 1;
        }
        if (healthReference.getHealth() == 100 && spawn == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                enemyReference.spawnAlert();
            }
            spawn = 1;
        }
    }

	void OnCollisionEnter(Collision collision) {

		PlayerBehavior pb = collision.gameObject.GetComponent<PlayerBehavior> ();
		if(pb != null){
			pb.Damage (20, Pathogen.Player.DamageType.Collision);
			pb.GetComponent<Rigidbody> ().AddForce (pb.transform.forward*-200, ForceMode.Impulse);
		}


	}

}
