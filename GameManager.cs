/* 
  GameManager.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Esse é o script que controla o estado do jogo (GameState). O código trata os eventos de input e eventos do jogo, possui as referências para elementos de UI. É uma classe "Singleton".

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Range {
	public float min;
	public float max;

	public float getRandom() {
		return Random.Range (min, max);
	}

	public int getRandomInt() {
		return Random.Range ((int)min, (int)max);
	}
}

public class GameManager : MonoBehaviour {

	[HideInInspector] public static GameManager instance;

	public Text scoreText;
	public Text pauseText;
	public Slider cannonSlider;
	public Slider missileSlider;

	public KeyCode shootKey = KeyCode.Space;
	public int shootDelay = 60;
	public int missileDelay = 120;

	public GameObject bulletPrefab;
	public GameObject missilePrefab;
	public Transform bulletExitPoint;

	public GameObject[] enemies;
	public Range enemySpawnTime; 
	public Transform enemySpawnPosition;
	public Range enemyHorizontalOffset; 

	private int shootTimer = 0;
	private int missileTimer = 0;
	private int enemyTimer = 0;

	private int score;

	[HideInInspector] public bool paused = false;

	void Awake () {

		// Singleton Implementation
		if (GameManager.instance == null) {
			GameManager.instance = this;
		} else if (GameManager.instance != this) {
			Destroy (gameObject);
		}

		initializeGame ();
	}

	void FixedUpdate () {
		if (paused == true)
			return;
		
		if (shootTimer > 0)
			shootTimer -= 1;
		if (missileTimer > 0)
			missileTimer -= 1;

		checkInputs ();

		updateEnemies ();

		updateUI ();
	}

	void updateEnemies() {
		if (enemyTimer == 0) {
			spawnEnemy ();
			enemyTimer = enemySpawnTime.getRandomInt ();
		} else {
			enemyTimer -= 1;
		}
	}

	void updateUI() {
		scoreText.text = score + "";
		cannonSlider.value =(float) (shootDelay - shootTimer * 1.0f) / shootDelay * 1.0f;
		missileSlider.value =(float) (missileDelay - missileTimer * 1.0f) / missileDelay * 1.0f;
	}

	void spawnEnemy() {
		GameObject selected = enemies [Random.Range (0, enemies.Length)];

		Vector2 position = enemySpawnPosition.position;
		position.x += enemyHorizontalOffset.getRandom ();

		GameObject.Instantiate (selected, position, Quaternion.identity);
	}

	void checkInputs() {
		if (Input.GetKey (shootKey)) {
			shoot ();
		}
	}

	public void addScore(int qnt) {
		score += qnt;
	}

	public void pauseGame() {
		paused = !paused;
		if (paused)
			pauseText.text = "Continuar";
		else
			pauseText.text = "Pausar";
	}

	public void restart() {
		SceneManager.LoadScene (0);
	}

	void initializeGame() {
		score = 0;
		shootTimer = 0;
		missileTimer = 0;
		enemyTimer = enemySpawnTime.getRandomInt ();
		paused = true;
	}

	public void shoot() {
		if (shootTimer > 0 ||  paused == true)
			return;

		shootTimer = shootDelay;
		GameObject.Instantiate (bulletPrefab, bulletExitPoint.position, bulletExitPoint.rotation);
	}

	public void shootMissiles() {
		if (missileTimer > 0 || paused == true)
			return;
		missileTimer = missileDelay;

		for (int i = 0; i < 3; i++) {
			Vector3 pos = bulletExitPoint.transform.position;
			pos.x -= (i - 1);
			GameObject.Instantiate (missilePrefab, pos, bulletExitPoint.transform.rotation);
		}
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
