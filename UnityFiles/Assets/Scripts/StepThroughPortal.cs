using UnityEngine;
using System.Collections;

public class StepThroughPortal : MonoBehaviour {
	public GameObject otherPortal;
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController player;
	private Rigidbody rb;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
		rb = player.GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			

//			rb.velocity = otherPortal.transform.forward * rb.velocity.magnitude;

//			rb = other.attachedRigidbody;
//			rb.isKinematic = true;
//			rb.MovePosition(otherPortal.transform.position + otherPortal.transform.forward * 3);
//			rb.isKinematic = false;
//
//			Rigidbody rb = other.attachedRigidbody;
			Vector3 vel = rb.velocity + rb.angularVelocity;
			vel = Vector3.Reflect (vel, transform.forward);		
			vel = transform.InverseTransformDirection (vel);		
			vel = otherPortal.transform.TransformDirection (vel);		
			rb.velocity = vel;		
			rb.rotation = otherPortal.transform.rotation;		
			rb.position = otherPortal.transform.position + otherPortal.transform.forward * 3;

//			other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 1;


		}
	}
}
