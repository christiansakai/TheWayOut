using UnityEngine;
using System.Collections;

public class pickuppost : MonoBehaviour {
	// Use this for initialization
	public static Vector3 PostRespawnPoint;

	void Awake () {
		PostRespawnPoint = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PostRespawn() {
		// Drop object;
		PickUpObject.carrying = false;
		PickUpObject.carriedObject = null;
		// change the post2 position back to the original
		transform.position = PostRespawnPoint;
	}
}
