using UnityEngine;
using System.Collections;

public class StepThroughPortal : MonoBehaviour {
	public GameObject otherPortal;

	void OnTriggerEnter(Collider other) {
//		if (other.tag == "Player") {
			// this line is questionable - still having difficulty reorienting player according to where they come out
//			other.transform.localEulerAngles += Quaternion.LookRotation (otherPortal.transform.forward).eulerAngles;

//		other.transform.rotation = otherPortal.transform.rotation;

		Rigidbody rb = other.attachedRigidbody;
		Vector3 vel = rb.velocity;
		vel = Vector3.Reflect (vel, transform.forward);
		vel = transform.InverseTransformDirection (vel);
		vel = otherPortal.transform.TransformDirection (vel);
		rb.velocity = vel;
//		rb.rotation = otherPortal.transform.rotation;

		other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 1;

//		other.transform.rotation = Quaternion.FromToRotation (other.transform.rotation.eulerAngles, otherPortal.transform.rotation.eulerAngles);

//		other.transform.rotation.Set(otherPortal.transform.rotation.x);

//		}
	}
}
