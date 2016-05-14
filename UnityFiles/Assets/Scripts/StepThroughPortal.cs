﻿using UnityEngine;
using System.Collections;

public class StepThroughPortal : MonoBehaviour {
	public GameObject otherPortal;
	private GameObject player;
	private Rigidbody rb;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rb = player.GetComponent<Rigidbody> ();
	}

	void OnControllerColliderHit(ControllerColliderHit other) {
		Debug.Log ("Entered");
			
//			rb.velocity = otherPortal.transform.forward * rb.velocity.magnitude;

//			rb = other.attachedRigidbody;
//			rb.isKinematic = true;
//			rb.MovePosition(otherPortal.transform.position + otherPortal.transform.forward * 3);
//			rb.isKinematic = false;
//
//			Rigidbody rb = other.attachedRigidbody;
//			Vector3 vel = rb.velocity + rb.angularVelocity;
//			vel = Vector3.Reflect (vel, transform.forward);		
//			vel = transform.InverseTransformDirection (vel);		
//			vel = otherPortal.transform.TransformDirection (vel);		
//			rb.rotation = otherPortal.transform.rotation;		
			player.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 3;
//			rb.AddForce ((otherPortal.transform.position - player.transform.position).normalized * 20 * Time.smoothDeltaTime);
//			rb.velocity = vel * rb.velocity.magnitude;


//			other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 1;


	}
}
