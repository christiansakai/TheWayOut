using UnityEngine;
using System.Collections;

public class OnMovingPlatform : MonoBehaviour {

	private Rigidbody rb;
	private UnityEngine.Transform platform;
	private Vector3 platformLocalPoint;
	private Vector3 platformGlobalPoint;
	private Vector3 platformLastVelocity;

	private Quaternion platformLocalRotation;
	private Quaternion platformGlobalRotation;

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

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Platform") {
			
			Debug.Log ("on a platform!");
			platform = col.collider.transform;

			//rb.isKinematic = true;
//			transform.parent = col.collider.transform;
		}
	}

	void OnCollisionStay(Collision col) {
		if (col.collider.tag == "Platform") {
			platformGlobalPoint = transform.position;
			platformLocalPoint = col.collider.transform.InverseTransformPoint (transform.position);
			Vector3 newPoint = platform.TransformPoint (platformLocalPoint);
			Vector3 moveDistance = newPoint - platformGlobalPoint;
			Debug.Log (moveDistance);
//			platformLastVelocity = (newPoint - platformGlobalPoint) / Time.deltaTime;
//			col.collider.transform.TransformPoint (col.collider);
			//rb.isKinematic = true;

		}
	}

	void OnCollisionExit(Collision col) {
		if (col.collider.tag == "Platform") {
			//rb.isKinematic = false;
		}
	}


}
