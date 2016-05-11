using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitAndSave : MonoBehaviour {
	public Transform Player;

	void Awake()
	{
		Player.position = new Vector3 (PlayerPrefs.GetFloat ("x"), PlayerPrefs.GetFloat ("y"), PlayerPrefs.GetFloat ("z"));
		Player.eulerAngles = new Vector3(0, PlayerPrefs.GetFloat("Cam_y",0));
	}

	public void SaveGameSettings(bool Quit)
	{
//		PlayerPrefs.SetFloat ("x",Player.position.x);
//		PlayerPrefs.SetFloat ("y",Player.position.y);
//		PlayerPrefs.SetFloat ("z",Player.position.z);
//		PlayerPrefs.SetFloat ("Cam_y", Player.eulerAngles.y);
		if (Quit) {
			Time.timeScale = 1;
			SceneManager.LoadScene ("Menu");
		}
	}

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
	
