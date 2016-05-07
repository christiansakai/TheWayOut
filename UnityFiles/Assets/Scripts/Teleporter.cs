using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
	
	public GameObject otherPortal;

	//https://www.youtube.com/watch?v=wBb6XxI_Xzs
	private bool teleporting = false;

	public void TeleportTo(GameObject obj) {
		obj.transform.position = transform.position - (transform.up * 0.65f);
		obj.GetComponent<Rigidbody>().velocity = transform.up * obj.GetComponent<Rigidbody>().velocity.magnitude;
		teleporting = true;
	}

	private void OnTriggerEnter(Collider c) {
		if (c.name == "Player") {
			if (!teleporting) {
				otherPortal.GetComponent<Teleporter> ().TeleportTo (c.gameObject);
			}
			teleporting = false;
		}
	}
}
