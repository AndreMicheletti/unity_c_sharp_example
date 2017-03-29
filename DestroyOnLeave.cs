/* 
  DestroyOnLeave.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Um script simples que faz com que qualquer objeto que saia da área especificada no Collider será destruido. Serve para jogos em que qualquer objeto que saia da tela deva ser destruído (liberado).

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLeave : MonoBehaviour {

	// Use this for initialization
	void OnTriggerExit2D(Collider2D other) {
		Destroy (other.gameObject);
	}
}
