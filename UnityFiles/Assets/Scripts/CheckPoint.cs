using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		// save the player into file
		//		BinaryFormatter bf = new BinaryFormatter ();
		//		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		//		PlayerData data = new PlayerData ();
		//		data.health = health;
		//		data.experience = experience;
		//
		//		bf.Serialize (file, data);
		//		file.Close ();

		// also set the respawn position to this checkpoint position;
		PlayerHealth.respawnPoint = transform.position; 

		
	}
		
}
