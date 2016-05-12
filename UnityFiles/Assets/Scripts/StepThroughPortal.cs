using UnityEngine;
using System.Collections;

public class StepThroughPortal : MonoBehaviour {
	public GameObject otherPortal;
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController controller;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {

			// much better solution, but still not ideal
			// player comes out of portal, but isn't always facing direciton relative to where they were facing
			controller = other.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ();
//			Debug.Log (other.attachedRigidbody.velocity + controller.Velocity);
			controller.mouseLook.Init (otherPortal.transform, controller.cam.transform);
			other.transform.rotation = otherPortal.transform.rotation;

			Rigidbody rb = other.attachedRigidbody;
			Vector3 vel = rb.velocity;

			vel = Vector3.Reflect (vel, transform.forward);
			vel = transform.InverseTransformDirection (vel);
			vel = otherPortal.transform.TransformDirection (vel);
			rb.velocity = vel;

			other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 1;


		}
	}
}
