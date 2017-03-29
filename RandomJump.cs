/* 
  RandomJump.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Fazer com que o GameObject (que possui um RigidBody) pule automaticamente quando é criado

*/

using UnityEngine;
using System.Collections;

public class RandomJump : MonoBehaviour {

	private Rigidbody body;

	public float minY;
	public float maxY;

	public float randX;

	void Start () {
		body = GetComponent<Rigidbody> ();
		float forceX = Random.Range(-randX, randX);
		float forceY = Random.Range(minY,maxY);

		Vector3 force = new Vector3 (forceX, forceY);
		body.velocity = force;
	}
}
