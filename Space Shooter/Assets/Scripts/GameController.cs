using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public bool spawnHazards;
	public GameObject hazard;
	public GameObject hazard2;
	public GameObject hazard3;

	public GameObject enemyShip;
	public float biggerThanToSpawnEnemy;

	public int hazardCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private int score = 0;
	private bool gameOver = false;

	private float maxX = 6;
	private float asteroidSpawnZ = 16f;
	private float enemySpawnZ = -6.5f;

	void Start() {
		restartText.text = "";
		gameOverText.text = "";

		UpdateScore ();
		StartCoroutine (LoopForever ());
	}

	void Update() {
		if (gameOver) {
			if(Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator LoopForever() {
		yield return new WaitForSeconds(startWait);

		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				if(spawnHazards) {
					spawnAsteroids ();
				}
				spawnEnemies ();
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if(gameOver) {
				restartText.text = "Press 'R' to restart.";
				break;
			}
		}
	}

	void spawnAsteroids() {
		float randomX = getRandomX ();
		Vector3 spawnPosition = new Vector3 (randomX, 0.0f, asteroidSpawnZ);
		Quaternion spawnRotation = Quaternion.identity;
		
		GameObject objectClass;
		int nb = Random.Range (1, 3);
		if(nb == 0) {
			objectClass = hazard;
		} else if(nb == 1) {
			objectClass = hazard2;
		} else {
			objectClass = hazard3;
		}
		
		Instantiate (objectClass, spawnPosition, spawnRotation);
	}

	void spawnEnemies() {
		bool mustSpawn = Random.value > biggerThanToSpawnEnemy;
		bool alreadySpawned = GameObject.FindGameObjectsWithTag ("Enemy").Length > 0;
		if (!alreadySpawned && mustSpawn) {
			float randomX = getRandomX ();
			Vector3 spawnPosition = new Vector3 (randomX, 0.0f, enemySpawnZ);
			Quaternion spawnRotation = Quaternion.identity;

			Instantiate (enemyShip, spawnPosition, spawnRotation);
		}
	}

	float getRandomX() {
		return Random.Range (-maxX, maxX);
	}

	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		gameOverText.text = "GAME OVER!";
		gameOver = true;
	}

}

public static class Common {

	public static GameController GetGameController() {
		GameController gameController = null;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		
		if (gameController == null) {
			Debug.LogError("Cannot find 'GameController' Script.");
		}

		return gameController;
	}

}
