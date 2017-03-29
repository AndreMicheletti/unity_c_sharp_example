/* 
  DestroyByParticle.cs
  
Autor: André Luiz Micheletti - 2017

Tipo: Script de behaviour - Unity3D

Objetivo: Um script simples que destroi um GameObject assim que um sistema de partículas terminar sua emulação. Ótimo para GameObjects que são efeitos (explosões por exemplo1) e devem ser li

*/

using UnityEngine;
using System.Collections;

public class DestroyByParticle : MonoBehaviour {

	public new ParticleSystem particleSystem;

	void FixedUpdate() {
		if (particleSystem.isStopped) {
			Destroy (gameObject);
		}
	}
}
