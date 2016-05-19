using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
	public Transform canvas;
	State state;
	// Update is called once per frame

	void Start(){
		state = GameObject.Find ("GameState").GetComponent<State> ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
		if (Input.GetKeyDown(KeyCode.P)){
			SaveGameSettings (true);
		}
	}

	public void Pause(){
		if (canvas.gameObject.activeInHierarchy == false) {
				canvas.gameObject.SetActive (true);
				Time.timeScale = 0;
			} else {
				canvas.gameObject.SetActive (false);
				Time.timeScale = 1;
			}

	}

	public void SaveGameSettings(bool Quit)
	{
		if (Quit) {
			Time.timeScale = 1;
			// Exit to the Start Menu
			state.LoadScene ("Menu");
			// Save player data to the backend database
			state.SaveUserInfo();
		}
	}
		
}
