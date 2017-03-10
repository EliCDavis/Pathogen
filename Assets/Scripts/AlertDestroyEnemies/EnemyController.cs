using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Pathogen.Scene.Veins;
using Pathogen.Scene.Brains;

public class EnemyController : MonoBehaviour
{
    public Vector3 lastKnown;
    public GameObject alertPrefab;
    public GameObject destroyPrefab;
    public int detected = 0;

	[SerializeField]
	private GameObject player;

	[SerializeField]
	private GameObject playerMarker;

	[SerializeField]
	private GameObject congrats;

	[SerializeField]
	private Text congratsScore;

	[SerializeField]
	private GameObject deathScreen;

	[SerializeField]
	private Text deathReason;

	[SerializeField]
	private GameObject pauseScreen;

	[SerializeField]
	private GameObject hud;

	[SerializeField]
	private Slider hudHealth;

	[SerializeField]
	private Text hudScore;

	[SerializeField]
	private GameObject howToPlay;

	[SerializeField]
	private GameObject[] brainParts;

    private GameObject alertCell;
    private GameObject destroyCell;
    private float coinflip;
    private float spawnX;
    private float spawnY;
    private float spawnZ;

    // Use this for initialization
    void Start()
    {
        for(int i=0; i < 25; i++)
        {
            spawnAlert();
        }

        for(int i=0; i < 30; i++)
        {
            spawnDestroy();
        }
		CloseAllScreens ();
		Time.timeScale = 0;
		howToPlay.SetActive (true);
    }

	public void BrainPartHit(){
		lastKnown = player.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		playerMarker.transform.position = lastKnown;
		if(Input.GetKeyUp (KeyCode.P) && CanTogglePause()){
			TogglePause ();
		}
	
		if (player != null) {
			hudScore.text = "" + ScoreManager.GetInstance ().GetStagedScore ();
			hudHealth.value = player.GetComponent<PlayerBehavior> ().GetHealth () / 100f;
		}

		if (hud.activeSelf) {
			for (int i = 0; i < brainParts.Length; i++) {
				if (brainParts [i] != null) {
					return;
				}
			}
			GoToEnd ();
		}
    }

	private bool CanTogglePause(){
		return hud.activeSelf;
	}

	/// <summary>
	/// Opens and closes the pause menu as well as
	/// </summary>
	private void TogglePause() {
		if (CurrentlyPaused()) {
			pauseScreen.SetActive (false);
			Time.timeScale = 1;
		} else {
			pauseScreen.SetActive (true);
			Time.timeScale = 0;
		}
	}

	// Whether or not we're currently paused
	private bool CurrentlyPaused() {
		return pauseScreen.activeSelf;
	}

    public void spawnAlert()
    {
        alertCell = GameObject.Instantiate(alertPrefab);
        alertCell.transform.position = generateSpawnPosition();
    }

	public void GoToEnd() {
		SceneManager.LoadScene(3);
	}

	public void ReloadLevel() {
		SceneManager.LoadScene (2);
	}

	public void MainMenu() {
		SceneManager.LoadScene (0);
	}

	public void StartGame() {
		ScoreManager.GetInstance ().ClearStagedScores ();
		player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		Time.timeScale = 1;
		CloseAllScreens ();
		hud.SetActive (true);
	}

	private void CloseAllScreens() {
		deathScreen.SetActive (false);
		congrats.SetActive (false);
		hud.SetActive (false);
		howToPlay.SetActive (false);
	}

	public void OpenDeathScreen(string reason) {
		CloseAllScreens ();
		deathScreen.SetActive (true);
		deathReason.text = reason;
	}

    public void spawnDestroy()
    {

        destroyCell = GameObject.Instantiate(destroyPrefab);
        destroyCell.transform.position = generateSpawnPosition();
    }
    private Vector3 generateSpawnPosition()
    {
        spawnX = Random.Range(-1000f, 1000f);
        
        spawnZ = Random.Range(-1000f, 1000f);
        if (spawnX > -415 && spawnX < 525 && spawnZ < 825 && spawnZ > -500)
        {
            coinflip = Random.Range(1f, 10f);
            if (coinflip % 2 == 0)
            {
                spawnY = Random.Range(5f, 100f);
            }

            else
            {
                spawnY = Random.Range(860f, 1000f);
            }

        }

        else
        {
            spawnY = Random.Range(5f, 1000f);
        }

        return new Vector3(spawnX, spawnY, spawnZ);
    }
}
