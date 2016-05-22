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

	void Start () {
		state = State.instance;
	}

	void OnGUI(){
		un = GUI.TextField (new Rect (283,230,160,30), un, 25);
		pw = GUI.PasswordField (new Rect (283,265,160, 30), pw, "*" [0], 25);
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