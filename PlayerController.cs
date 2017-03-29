/* 
  PlayerController.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Esse é o script que controla o jogador (que é uma nave espacial nesse caso). Aqui é tratado o input para controlar o jogador e todo o seu comportamento.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public Text healthText;

	public float maxSpeed = 4f;
	public float maxAngularSpeed = 2f;

	public Transform cameraFollow;
	public GameObject shotSpawn;
		
	public Weapon equippedWeapon = null;

	private Rigidbody2D body;

	private CharacterBase ch;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		ch = GetComponent<CharacterBase> ();
	}

	void FixedUpdate() {
		int horizontal = -(int) Input.GetAxisRaw ("Horizontal");

		if (horizontal != 0) {
			body.angularVelocity = horizontal * maxAngularSpeed;
		}

		Move ();
		if (cameraFollow != null)
			MoveCameraFollow ();

		if (equippedWeapon != null)
			equippedWeapon.UpdateWeapon ();
		
		updateInputs ();
		updateHealthText ();
	}

	void updateInputs() {
		if (equippedWeapon != null)
		if (Input.GetButton ("Fire1"))
				shoot ();
	}

	void shoot() {
		equippedWeapon.shootCommand (shotSpawn);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void updateHealthText() {
		healthText.text = ch.getHealthInt() + "";
	}

	void Move() {
		int vertical = (int) Input.GetAxisRaw ("Vertical");

		float angle = transform.eulerAngles.magnitude * Mathf.Deg2Rad;

		if (vertical != 0) {
			//Vector3 pos = transform.position;
			float x = (Mathf.Sin (angle) * -maxSpeed) * Time.deltaTime * vertical;
			float y = (Mathf.Cos (angle) * maxSpeed) * Time.deltaTime * vertical;
			body.AddForce(new Vector2 (x * maxSpeed, y * maxSpeed), ForceMode2D.Force);
			//transform.position = pos;
		}
	}

	void MoveCameraFollow() {
		int vertical = (int) Input.GetAxisRaw ("Vertical");
		cameraFollow.localPosition = new Vector3 (0f, Mathf.Max(0.2f, body.velocity.magnitude * 1.5f * vertical), 0f);
	}
}
