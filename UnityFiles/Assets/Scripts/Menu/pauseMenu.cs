using UnityEngine;
using System.Collections;

public class pauseMenu : MonoBehaviour {

	public Canvas PauseScreen;
	private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController controller;
//    Camera controllerCamera;

	// Use this for initialization
	void Start () {
		PauseScreen = PauseScreen.GetComponent<Canvas> ();
		controller = GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController> ();
//		controllerCamera = controller.FindWithTag("Main Camera")
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("escape")){
			controller.enabled = false;
			PauseScreen.enabled = true;
		}
	}
	
	public void ResumeGame (){
		controller.enabled = true;
		PauseScreen.enabled = false;
	}
	
	public void ExitGame(){
		Application.Quit ();
	}
}
