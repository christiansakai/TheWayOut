using UnityEngine;
using System.Collections;

public class ExitAndSave : MonoBehaviour {

	// save the player into file
	//		BinaryFormatter bf = new BinaryFormatter ();
	//		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
	//		PlayerData data = new PlayerData ();
	//		data.health = = PlayerHealth.currentHealth;
	//		data.experience = experience;
	//
	//		bf.Serialize (file, data);
	//		file.Close ();

//	public void Load(){
//		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
//			BinaryFormatter bf = new BinaryFormatter ();
//			FileStream file = File.open (Application.persistentDataPath + "/playerInfo.dat");
//			PlayerData data = (PlayerData)bf.Deserialize (file);
//			file.Close ();
//
//
//		}
//	}
}

//[Serializable]
//class PlayerData (){
//	public int health;
//	public Transform playerPosition;
//}
