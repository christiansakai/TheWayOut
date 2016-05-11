using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class CheckPoint : MonoBehaviour {
	public Transform Player;
//	PlayerHealth PlayerHealth;

	void Start(){
//		PlayerHealth = Player.GetComponent<PlayerHealth> ();
	}

	void OnTriggerEnter(Collider other){
		// also update the respawn position to this checkpoint position;
		PlayerHealth.respawnPoint = transform.position;
		PlayerHealth.respawnPointAngle = transform.eulerAngles;

		// save all the player info at the checkpoint to PlayerPrefs;
		  //position
		PlayerPrefs.SetFloat ("x",Player.position.x);
		PlayerPrefs.SetFloat ("y",Player.position.y);
		PlayerPrefs.SetFloat ("z",Player.position.z);
		PlayerPrefs.SetFloat ("Cam_y", Player.eulerAngles.y);
		 //health
//		PlayerPrefs.SetInt ("currentHealth", PlayerHealth.currentHealth);
		 //respawnPoint
		PlayerPrefs.SetFloat ("RPx", PlayerHealth.respawnPoint.x);
		PlayerPrefs.SetFloat ("RPy", PlayerHealth.respawnPoint.y);
		PlayerPrefs.SetFloat ("RPz", PlayerHealth.respawnPoint.z);
		PlayerPrefs.SetFloat ("RPA_y", PlayerHealth.respawnPointAngle.y);
		// stamina
	}
		
}
