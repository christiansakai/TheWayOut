using UnityEngine;
using System.Collections;

public class OnMovingPlatform : MonoBehaviour {

	private UnityEngine.Transform platform;
	private bool onPlatform = false;
	private Vector3 platformOldPosition;
	private Vector3 platformNewPosition;

	void Start () {

	}

	void FixedUpdate () {
		if (onPlatform) {
			platformNewPosition = platform.position;
			transform.position += platformNewPosition - platformOldPosition;
			platformOldPosition = platformNewPosition;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Platform") {
			onPlatform = true;
			platform = col.collider.transform;
			platformOldPosition = platformNewPosition = platform.position;
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.collider.tag == "Platform") {
			onPlatform = false;
		}
	}
}
