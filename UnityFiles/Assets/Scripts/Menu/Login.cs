using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class Login : MonoBehaviour {
	public GameObject username;
	public GameObject password;
	string un;
	string pw = "";
	State state;

	void Start () {
		state = State.instance;
	}

	void OnGUI(){
		pw = GUI.PasswordField (new Rect (378.5f,269.5f,160, 30), pw, "*" [0], 25);
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			if (username.GetComponent<InputField> ().isFocused) {
				password.GetComponent<InputField> ().Select ();
			} else if(password.GetComponent<InputField>().isFocused){
				username.GetComponent<InputField>().Select();
			}
		} else if (Input.GetKeyDown (KeyCode.Return)) {
			LogInEnter();
		}
	}

	public void LogInEnter(){
		un = username.GetComponent<InputField> ().text;
		Debug.Log (un);
//		pw = password.GetComponent<InputField> ().text;
		Debug.Log(pw);
		state.Login(un,pw);
	}

	public void SignupPage(){
		state.LoadScene ("Signup");
	}

}