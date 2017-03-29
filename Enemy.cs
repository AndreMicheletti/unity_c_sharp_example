/* 
  Enemy.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Esse é o script que controla um inimigo. Possui as funções para criar, mover e destruir um inimigo.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int scoreValue = 10;
	public int health = 1;

	public float verticalSpeed = 1f;
	public float horizontalSpeed = 1f;

	public GameObject destroyEffect;


	private int direction = 1;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GameManager.instance.paused == false)
			Move ();
	}

	void Move() {

		Vector3 newPosition = new Vector3 (
			transform.position.x + (horizontalSpeed * direction * Time.deltaTime),
			transform.position.y - (verticalSpeed * Time.deltaTime),
			0);
		transform.position = newPosition;
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "EnemyBounds") {
			direction *= -1;
			Move ();
		}
	}

	public void hit(int dmg) {
		health -= dmg;
		if (health <= 0) {
			Die ();
		}
	}

	void Die() {
		Destroy (gameObject);
		Camera.main.gameObject.GetComponentInParent<Animator> ().SetTrigger ("Shake");
		if (destroyEffect != null) {
			GameObject.Instantiate (destroyEffect, transform.position, Quaternion.identity);
		}
		GameManager.instance.addScore (scoreValue);
	}
}
