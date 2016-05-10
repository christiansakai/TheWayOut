using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class CheckPoint : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		// save the player into file
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();
		data.health = health;
		data.experience = experience;

		bf.Serialize (file, data);
		file.Close ();

		// also set the respawn position to this checkpoint position;
		PlayerHealth.respawnPoint = transform.position;
		
	}
		
	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.open (Application.persistentDataPath + "/playerInfo.dat");
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();


		}
	}
		
}

[Serializable]
class PlayerData (){
	public int health = PlayerHealth.currentHealth;
}