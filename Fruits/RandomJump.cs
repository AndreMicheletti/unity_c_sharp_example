using UnityEngine;
using System.Collections;

public class RandomJump : MonoBehaviour {

	private Rigidbody body;

	public float minY;
	public float maxY;

	public float randX;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
		float forceX = Random.Range(-randX, randX);
		float forceY = Random.Range(minY,maxY);

		Vector3 force = new Vector3 (forceX, forceY);
		body.velocity = force;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
