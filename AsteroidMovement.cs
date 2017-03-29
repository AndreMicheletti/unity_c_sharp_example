/* 
  AsteroidMovement.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Um script que trata o comportamento de um Asteroide. Quando o GameObject é iniciado o script escolhe aleatóriamente um Sprite para o Asteroide e adiciona um movimento aleatório a ele. O script também trata a destruição do Asteroide por dano do jogador.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

	public int damageDealt = 1;

	public float minRotation = 30f;
	public float maxRotation = 200f;

	public float maxVelocity = 5f;

	public Sprite[] sprites;
	public GameObject destroyEffect;

	private Rigidbody2D body;
	private float rotation = 0f;

	private float xVel = 0f;
	private float yVel = 0f;

	private CharacterBase charBase;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		charBase = GetComponent<CharacterBase> ();
		charBase.initializeCharacter ();
		rotation = Random.Range (minRotation, maxRotation);

		transform.position = new Vector3 (transform.position.x, transform.position.y, Random.Range (-0.02f, 0.02f));

		SpriteRenderer render = GetComponent<SpriteRenderer> ();
		render.sprite = sprites [Random.Range (0, sprites.Length)];

		body.angularVelocity = rotation;

		xVel = Random.Range (-maxVelocity, maxVelocity);
		yVel = Random.Range (-maxVelocity, maxVelocity);
	}

	void FixedUpdate () {
		body.velocity = new Vector2 (xVel, yVel);

		if (charBase.isDead ()) {
			destroy ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == gameObject.tag)
			return;
		if (other.gameObject.GetComponent<CharacterBase> () != null) {
			other.gameObject.GetComponent<CharacterBase> ().Hit (damageDealt);
			destroy ();
		}
	}

	public void destroy() {
		GameObject.Instantiate (destroyEffect, transform.position, Quaternion.identity);
		Camera.main.GetComponent<CameraMovement> ().screenShake ();
		Destroy (gameObject);
	}
}
