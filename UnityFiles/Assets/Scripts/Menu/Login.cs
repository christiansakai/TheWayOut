using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class Login : MonoBehaviour {
	string un = "Enter Email";
	string pw = "Enter Password";
	State state;
	int posX;
	int posY;

	void Start () {
		state = State.instance;
		posX = Screen.width / 2 - 80;
		posY = Screen.height / 2;
	}

	void OnGUI(){
		un = GUI.TextField (new Rect (posX,posY-17,160,30), un, 25);
		pw = GUI.PasswordField (new Rect (posX,posY+17,160, 30), pw, "*" [0], 25);
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			LogInEnter();
		}
	}

	public void LogInEnter(){
		state.Login(un,pw);
	}

	public void SignupPage(){
		state.LoadScene ("Signup");
	}

}