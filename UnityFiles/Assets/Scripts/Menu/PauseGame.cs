using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
	public Transform canvas;
	private State state;
	// Update is called once per frame

	void Start(){
		state = State.instance;
	}

	void Update () {
		Debug.Log (Cursor.visible);
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			SaveGameSettings ();
		}
	}

	public void Pause(){
		if (canvas.gameObject.activeInHierarchy == false) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			canvas.gameObject.SetActive (true);
			Time.timeScale = 0;
		} else {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			canvas.gameObject.SetActive (false);
			Time.timeScale = 1;
		}

	}

	public void SaveGameSettings()
	{
		Time.timeScale = 1;
		// Save player data to the backend database
		state.SaveUserInfo();
		// Exit to the Start Menu
		state.LoadScene ("Menu");
	}
		
}
