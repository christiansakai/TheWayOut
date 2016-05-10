using UnityEngine;
using System.Collections;

public class OnMovingPlatform : MonoBehaviour {

	private Rigidbody rb;
	private UnityEngine.Collider platform;
	private Vector3 platformLocalPoint;
	private Vector3 platformGlobalPoint;
	private bool onPlatform = false;

	private Vector3 platformOldPosition;
	private Vector3 platformNewPosition;
	private Vector3 moveDistance;

//	private Vector3 platformLastVelocity;
//	private Quaternion platformLocalRotation;
//	private Quaternion platformGlobalRotation;

	void Start () {
//		rb = GetComponent<FirstPersonController> ();
//		rb = GetComponent<Rigidbody> ();

	}

	void FixedUpdate () {
		if (onPlatform) {
//			Vector3 newPoint = platform.transform.TransformPoint (platformLocalPoint);
//			Vector3 moveDistance = newPoint - platformGlobalPoint;

//			transform.position += moveDistance;
			platformNewPosition = platform.transform.position;
			moveDistance = platformNewPosition - platformOldPosition;
	
//			moveDistance = new Vector3(Mathf.Round(moveDistance.x), Mathf.Round(moveDistance.y), Mathf.Round(moveDistance.z));
			Debug.Log (moveDistance);
			transform.position += moveDistance;
			platformOldPosition = platformNewPosition;
//			rb.MovePosition(transform.position + moveDistance);
//			platformGlobalPoint = transform.position;
//			platformLocalPoint = platform.transform.InverseTransformPoint (transform.position);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Platform") {
			onPlatform = true;

			platform = col.collider;
			platformOldPosition = platformNewPosition = col.collider.transform.position;
//			platformGlobalPoint = transform.position;
//			platformLocalPoint = platform.transform.InverseTransformPoint (transform.position);
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.collider.tag == "Platform") {
			onPlatform = false;
		}
	}
}
