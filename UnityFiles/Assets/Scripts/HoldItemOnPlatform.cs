using UnityEngine;
using System.Collections;

public class HoldItemOnPlatform : MonoBehaviour {

	Rigidbody rb;
	bool onBox = false;

	void OnTriggerEnter(Collider col) {
		if(col.CompareTag("Player")) {
			rb = col.GetComponent<Rigidbody> ();
			rb.isKinematic = true;
//			rb.MovePosition (transform.position);
			onBox = true;
			Debug.Log ("onboard!");
	//		col.transform.parent = gameObject.transform;
//			col.transform.parent = transform.parent.transform;
		}
	}
	void OnTriggerStay(Collider col) {
		if(onBox && col.CompareTag("Player")) {
//			rb = col.GetComponent<Rigidbody> ();
//			rb.isKinematic = true;
			rb.MovePosition (transform.position);
			Debug.Log (rb.transform.position);

		}
	}

	void OnTriggerExit(Collider col) {
		if (onBox && col.CompareTag ("Player")) {
			col.transform.parent = null;
			rb.isKinematic = false;
			onBox = false;
		}
	}
}
