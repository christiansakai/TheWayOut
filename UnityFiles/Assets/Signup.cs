using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class Signup : MonoBehaviour {
	public GameObject email;
	public GameObject password;
	public GameObject username;
	State state;

	void Start () {
		state = State.instance;
	}
		
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			if (username.GetComponent<InputField> ().isFocused) {
				email.GetComponent<InputField> ().Select ();
			} else if (email.GetComponent<InputField> ().isFocused) {
				password.GetComponent<InputField> ().Select ();
			} else {
				username.GetComponent<InputField> ().Select ();
			}
		} else if (Input.GetKeyDown (KeyCode.Return)) {
			PostSignup();
		}
	}

	public void BackToLogin(){
		state.LoadScene ("Login");
	}

	public void PostSignup(){
		StartCoroutine(UserAuthentification());
	}

	IEnumerator UserAuthentification()
	{
		string pw = password.GetComponent<InputField> ().text;
		string mail = email.GetComponent<InputField> ().text;
		WWWForm form = new WWWForm();
		form.AddField("name", username.GetComponent<InputField> ().text);
		form.AddField("email", mail);
		form.AddField("password", pw);
		using (UnityWebRequest request = UnityWebRequest.Post (state.url + "api/users", form)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
				state.Login (mail, pw);
			}
		}
	}
}