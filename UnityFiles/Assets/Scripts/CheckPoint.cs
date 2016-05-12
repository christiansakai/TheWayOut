using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour {
	public Transform Player;
	public bool enterOnce = true;
	public float showDuration = 3.0f;

	private bool alreadyEntered = false;
	private float enteredTime;
	private float currentTime;
	private GameObject checkpoint;
//	PlayerHealth PlayerHealth;

	void Start(){
//		checkpoint = GameObject.Find ("Checkpoint");
//		this.gameObject.SetActive (false);
//		Debug.Log();
//		checkpoint.GetComponent<Text> ().enabled = false;
//		PlayerHealth = Player.GetComponent<PlayerHealth> ();
//		Debug.Log(this.gameObject);
	}

	void Update() {
		currentTime = Time.time;

		if (currentTime - enteredTime > showDuration) {
//			checkpoint.SetActive (false);
//			this.gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Entered");
		if (!alreadyEntered) {
			//Start timer and show Text
			enteredTime = Time.time;

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

			Debug.Log (alreadyEntered);
			alreadyEntered = true;
			Debug.Log (alreadyEntered);
		}
	}
		
}
