using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class CheckPoint : MonoBehaviour {
	public Transform Player;

	void OnTriggerEnter(Collider other){
		// also set the respawn position to this checkpoint position;
		PlayerHealth.respawnPoint = transform.position;
		// save all the player info at the checkpoint to PlayerPrefs;
		PlayerPrefs.SetFloat ("x",Player.position.x);
		PlayerPrefs.SetFloat ("y",Player.position.y);
		PlayerPrefs.SetFloat ("z",Player.position.z);
		PlayerPrefs.SetFloat ("Cam_y", Player.eulerAngles.y);
		PlayerPrefs.SetInt ("currentHealth", PlayerHealth.currentHealth);
		PlayerPrefs.SetFloat ("RPx", PlayerHealth.respawnPoint.x);
		PlayerPrefs.SetFloat ("RPy", PlayerHealth.respawnPoint.y);
		PlayerPrefs.SetFloat ("RPz", PlayerHealth.respawnPoint.z);
	}
		
}
