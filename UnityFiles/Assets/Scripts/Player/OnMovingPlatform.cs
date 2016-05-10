using UnityEngine;
using System.Collections;

public class OnMovingPlatform : MonoBehaviour {

	private Rigidbody rb;
	private UnityEngine.Transform platform;
	private Vector3 platformLocalPoint;
	private Vector3 platformGlobalPoint;
	private bool onPlatform = false;

	void FixedUpdate () {
		if (onPlatform) {
			transform.position += platform.TransformPoint (platformLocalPoint) - platformGlobalPoint;;
			platformGlobalPoint = transform.position;
			platformLocalPoint = platform.InverseTransformPoint (transform.position);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Platform") {
			onPlatform = true;
			platform = col.collider.transform;
			platformGlobalPoint = transform.position;
			platformLocalPoint = platform.InverseTransformPoint (transform.position);
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.collider.tag == "Platform") {
			onPlatform = false;
		}
	}
}