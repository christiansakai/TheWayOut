using UnityEngine;
using System.Collections;

public class GravityLift : MonoBehaviour {

	public float liftForce = 40;
	private GameObject player;
	private PlayerControls controls;
	private float runMultipler;
	private Rigidbody rb;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		controls = player.GetComponent<PlayerControls>();
		rb = player.GetComponent<Rigidbody> ();
		runMultipler = controls.runMultipler;

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player")) {
			Vector3 currentSpeed = rb.velocity;

			//Still an issue with running towards a gravity lift. Does not launch the player up.
			if (controls.isRunning) {
				rb.velocity = new Vector3(currentSpeed.x/runMultipler, currentSpeed.y + liftForce, currentSpeed.z);
			}
			else {
				rb.velocity = new Vector3(currentSpeed.x, currentSpeed.y + liftForce, currentSpeed.z);
			}
		}
	}
}
