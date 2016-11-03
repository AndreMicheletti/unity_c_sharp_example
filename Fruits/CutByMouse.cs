using UnityEngine;
using System.Collections;

public class CutByMouse : MonoBehaviour {

	public int scoreValue;

	private Rigidbody rigidBody;
	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		GameObject obj = GameObject.FindWithTag ("MainCamera");
		mainCamera = obj.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Debug.DrawRay (ray.origin, ray.direction * 10000.0f);
			if (Physics.Raycast(ray, out hit)) {
				Debug.Log (hit.collider.tag.ToString());
			}

		}
	}
}
