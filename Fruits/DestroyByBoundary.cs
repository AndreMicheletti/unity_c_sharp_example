using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		//Debug.Log ("Destroying: " + other.gameObject);
		if (other.gameObject.CompareTag("Fruit") == true)
			Destroy (other.gameObject);
	}
}
