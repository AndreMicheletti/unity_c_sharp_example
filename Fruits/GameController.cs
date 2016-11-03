using UnityEngine;
using System.Collections;

[System.Serializable]
public class Range {
	public float min, max;

	public float getValue() {
		return Random.Range (min, max);
	}
}

public class GameController : MonoBehaviour {

	public bool showDebugLogs;

	public GameObject[] fruitList;

	public Vector3 spawnValue;
	public Range spawnWait;
	public float startWait;
	public Range waveWait;
	public Range waveCount;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start() {
		score = 0;
		StartCoroutine (SpawnWaves() );
		//UpdateScore ();

		gameOver = false; restart = false;
	}

	void Update() {
		if (restart && Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel( Application.loadedLevel );
		}
		if (showDebugLogs)
			Debug.Log (Input.mousePosition);
	}

	IEnumerator SpawnWaves() {
		
		yield return new WaitForSeconds (startWait);
		while (true) {
			float countNow = waveCount.getValue ();
			for (int i = 0; i < countNow; i++) {
				Vector3 spawnPosition = new Vector3 ( Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);

				GameObject fruit = fruitList[Random.Range (0, fruitList.Length)];
				Quaternion spawnRotation = fruit.transform.rotation;

				Instantiate (fruit, spawnPosition, spawnRotation);

				yield return new WaitForSeconds (spawnWait.getValue());
			}
			yield return new WaitForSeconds (waveWait.getValue());

			if (gameOver) {
				restart = true;
				break;
			}
		}
	}

	public void GameOver() {
		gameOver = true;
	}

	public void Restart() {

	}

	/*public void AddScore(int newScore) {
		score += newScore;
		UpdateScore ();
	}*/

	/*void UpdateScore() {
		if ( scoreText != null)
			scoreText.text = "Score: " + score;
	}*/
}
