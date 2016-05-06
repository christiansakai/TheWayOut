using UnityEngine;
using System.Collections;

public class StepThroughPortal : MonoBehaviour {
	public GameObject otherPortal;

	// Use this for initialization
	void Start () {
		Debug.Log (UnityEngine.SceneManagement.SceneManager.sceneCount);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {

			// this line is questionable - still having difficulty reorienting player according to where they come out
//			other.transform.localEulerAngles += Quaternion.LookRotation (otherPortal.transform.forward).eulerAngles;
			other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 1;

			Rigidbody rb = other.attachedRigidbody;
			Vector3 vel = rb.velocity;
			vel = Vector3.Reflect (vel, transform.forward);
			vel = transform.InverseTransformDirection (vel);
			vel = otherPortal.transform.TransformDirection (vel);
			rb.velocity = vel;

		}
	}
}
