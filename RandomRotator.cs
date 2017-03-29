/* 
  RandomRotator.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Fazer com que o GameObject (que possui um RigidBody) rodar automaticamente quando é criado

*/

using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	private Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
