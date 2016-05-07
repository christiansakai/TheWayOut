using UnityEngine;
using System.Collections;

public class HoldItemOnPlatform : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if(col.CompareTag("Player")) {
			col.GetComponent<Rigidbody>().isKinematic = true;
	//		col.transform.parent = gameObject.transform;
			col.transform.parent = transform.parent.transform;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.CompareTag ("Player")) {
			col.transform.parent = null;
		}
	}
}
