/* 
  CameraMovement.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Um script que cria um movimento de câmera suave que segue outro GameObject. Possui também um componente de Animator que cria efeitos de camera (como ScreenShake).

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Transform follow;
	public float smoothTime = 0.2f;
	public float maxSpeed = 1f;

	public Animator screenAnimator;


	private Vector2 moveVelocity;
	private float startZ;


	void Start () {
		startZ = transform.position.z;
	}

	void FixedUpdate () {
		Move ();
		Zoom ();

		// Debug
		if (Input.GetKeyDown (KeyCode.Space)) {
			screenShake ();
		}
	}

	void Move() {
		Vector2 move = Vector2.SmoothDamp (transform.position, follow.position, ref moveVelocity, smoothTime, maxSpeed, Time.deltaTime);
		Vector3 newPosition = new Vector3 (move.x, move.y, startZ);
		transform.position = newPosition;
	}

	void Zoom() {
		// nothing
	}

	public void screenShake() {
		screenAnimator.SetTrigger ("Shake");
	}
}
