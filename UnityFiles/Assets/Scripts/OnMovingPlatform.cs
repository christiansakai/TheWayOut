using UnityEngine;
using System.Collections;

public class OnMovingPlatform : MonoBehaviour {

	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// This is terrible! but it kind of works for now
	void Update () {
		if ((transform.parent || rb.isKinematic) && (Input.GetAxis ("Vertical") != 0|| Input.GetAxis ("Horizontal") != 0|| Input.GetKeyDown (KeyCode.Space))) {
			rb.isKinematic = false;
			transform.parent = null;
		}
	}


}
