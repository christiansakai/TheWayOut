using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class Login : MonoBehaviour {
	public GameObject username;
	public GameObject password;
	State state;

	void Start () {
		state = State.instance;
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
		StartCoroutine(UserAuthentification(username.GetComponent<InputField> ().text, password.GetComponent<InputField> ().text));
	}

	public void SignupPage(){
		state.LoadScene ("Signup");
	}

	public IEnumerator UserAuthentification(string user, string pw)
	{
		WWWForm form = new WWWForm();
		form.AddField("email", user);
		form.AddField("password", pw);
		using (UnityWebRequest request = UnityWebRequest.Post (state.url + "login", form)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
				JSONNode CurrentUser = JSON.Parse(request.downloadHandler.text);
				state.StoreUser (CurrentUser ["user"]);
				state.LoadScene ("Menu");
			}
		}
	}

}
