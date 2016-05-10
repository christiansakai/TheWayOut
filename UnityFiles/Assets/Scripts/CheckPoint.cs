using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class CheckPoint : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		// also set the respawn position to this checkpoint position;
		PlayerHealth.respawnPoint = transform.position;
		
	}
		
}
	