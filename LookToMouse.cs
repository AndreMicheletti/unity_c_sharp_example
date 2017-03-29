/* 
  LookToMouse.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Um script simples que converte a posição do Mouse em posição no mundo da emulação, e faz com que o objeto "olhe para o mouse". Ou seja, rotaciona o objeto para o mouse.

*/

using UnityEngine;
using System.Collections;

public class LookToMouse : MonoBehaviour {

	void FixedUpdate() {
		Vector3 p2 = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 p1 = transform.position;
		float angle = Mathf.Atan2 (p2.y - p1.y, p2.x - p1.x) * 180.0f / Mathf.PI;
		transform.rotation = Quaternion.Euler (0, 0, angle - 90.0f);
	}

}
