/* 
  Spawner.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Um script que cria um Spawner de objetos que seleciona um objeto em uma lista e Instancia esse objeto no mundo.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] myObjects;
	public Transform parent;

	// Use this for initialization
	void Start () {
		Spawn ();
	}
	
	// Update is called once per frame
	public void Spawn () {
		SpawnRandom (myObjects);
	}

	public void SpawnRandom(GameObject[] objs) {
		GameObject selected = objs[Random.Range(0, objs.Length)];
		GameObject.Instantiate (selected, transform.position, Quaternion.identity, parent);
	}

	public void moveTo(float newY) {
		Vector3 newPos = transform.localPosition;
		newPos.y = newY;
		transform.localPosition = newPos;
		Debug.Log ("Moving to: " + newPos);
	}
}
