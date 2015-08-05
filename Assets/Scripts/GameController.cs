using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	// Used for starting the game
	public static bool gameStarted;
	public List<AudioClip> bgAudio = new List <AudioClip>();
	public GameObject playerPrefab;
	public GameObject playerSpawn;
	public GameObject motherShipPrefab;
	private GameObject player;
	private GameObject motherShip;
	public bool motherShipGone;	
	public CanvasGroup menu;
	public CanvasGroup guiCanvas;
	public bool mothershipMoving;

	public int currentScore;



	// Spawning Enemies
	public int enemyCount = 0;
	public GameObject[] respawns;
	public GameObject enemyPrefab;
	private GameObject enemy;


	// Game Over 
	public bool gameOver;
	public CanvasGroup gameOverScreen;

	// Use this for initialization
	void Start () {
		mothershipMoving = false;
		menu.alpha = 1;
		gameOverScreen.alpha = 0;
		guiCanvas.alpha = 0;
		gameOver = false;
		gameStarted = false;
		motherShipGone = false;
		PlayStartGameAudio ();
		LoadFriendlies ();
		respawns = GameObject.FindGameObjectsWithTag ("SpawnPoint");
		currentScore = 0;
	}

	// Update is called once per frame
	void Update () {
		BeginGameControl ();
		if (mothershipMoving) {
			motherShip.audio.Play();
			motherShip.transform.Translate (Vector3.left * 100.0f * Time.deltaTime, Space.World);		
		}
		StartGameCheck ();
		if (gameOver) {
			GameOver();		
		}
	}


	public void MuteAudio() {
		if (audio.mute) {
			audio.mute = false;
		}
		else {
			audio.mute = true;
		}
	}

	void GameOver () {
		GameObject[] enemyShipsRemaining;
		enemyShipsRemaining = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemyShipsRemaining.Length > 0) {
			foreach (GameObject enemy in enemyShipsRemaining) {
				Destroy(enemy.gameObject);
			}
		}
		gameOverScreen.alpha = 1;
		//audio.Stop();
	}

	public void OpenTwitter() {
		Application.OpenURL("http://twitter.com/groydis");
	}
	
	void BeginGameControl() {
		if(Input.GetKeyDown(KeyCode.Return) && !gameStarted) {
			menu.alpha = 0;
			guiCanvas.alpha = 1;
			StartCoroutine(TheGameStart());
			Invoke("PlayBattleGameAudio", 6);
		}
		if (Input.GetKeyDown (KeyCode.Return) && gameOver) {
			Application.LoadLevel("GameScreen");
		}
	}

	void StartGameCheck() {
		if (motherShipGone) {
			mothershipMoving = false;
			SpawnEnemies();
		}
	}

	void SpawnEnemies() {
		if (enemyCount < 5) {
			GameObject enemy = Instantiate(enemyPrefab, respawns[Random.Range(0, respawns.Length -1)].transform.position, Quaternion.identity) as GameObject;
			enemyCount++;
		}
	}

	void PlayStartGameAudio() {
		audio.clip = bgAudio[1];
		audio.Play ();
	}

	void PlayBattleGameAudio () {
		audio.clip = bgAudio[0];
		audio.Play ();
	}

	void LoadFriendlies() {
		motherShip = Instantiate (motherShipPrefab, motherShipPrefab.transform.position, motherShipPrefab.transform.rotation) as GameObject;
		player = Instantiate (playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation) as GameObject;
	}

	void StartSequence () {
		player.transform.Translate (Vector3.forward * Time.deltaTime, Space.World);
		gameStarted = true;
	}

	IEnumerator TheGameStart() {
		Invoke("StartSequence", 2);

		yield return new WaitForSeconds(4);
		mothershipMoving = true;
	}
}
