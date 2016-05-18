using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
	public Transform canvas;
//	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController controller;
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
		if (Input.GetKeyDown(KeyCode.Q)){
			Time.timeScale = 1;
			SceneManager.LoadScene ("Menu");
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
		
}
