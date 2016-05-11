using UnityEngine;
using System.Collections;

public class GravityLift : MonoBehaviour {

	public float liftForce = 40;
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController player;
	private float runMultipler;

	void Start () {
		player = GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ();
		runMultipler = player.movementSettings.RunMultiplier;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Player")) {
			Vector3 currentSpeed = other.GetComponent<Rigidbody> ().velocity;

			//Still an issue with running towards a gravity lift. Does not launch the player up.
			if (player.movementSettings.Running) {
				other.GetComponent<Rigidbody> ().velocity = new Vector3(currentSpeed.x/runMultipler, currentSpeed.y + liftForce, currentSpeed.z);
			}
			else {
				other.GetComponent<Rigidbody> ().velocity = new Vector3(currentSpeed.x, currentSpeed.y + liftForce, currentSpeed.z);
			}
		}
	}
}
