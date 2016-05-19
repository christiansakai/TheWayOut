using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class Login : MonoBehaviour {
	public GameObject username;
	public GameObject password;
	private string Username;
	private string Password;
	State state;

	// Use this for initialization
	void Start () {
		state = GameObject.Find ("GameState").GetComponent<State> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			if(username.GetComponent<InputField>().isFocused){
				password.GetComponent<InputField>().Select();
			}
			if(password.GetComponent<InputField>().isFocused){
				username.GetComponent<InputField>().Select();
			}
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			Username = username.GetComponent<InputField> ().text;
			Password = password.GetComponent<InputField> ().text;
			//send a get request to verify login information
			StartCoroutine(UserAuthentification(Username, Password));
		}
	}

	public void LogInEnter(){
		Username = username.GetComponent<InputField> ().text;
		Password = password.GetComponent<InputField> ().text;
		StartCoroutine(UserAuthentification(Username, Password));
//		SceneManager.LoadScene ("Menu");
	}

	IEnumerator UserAuthentification(string username, string password)
	{
		WWWForm form = new WWWForm();
		form.AddField("email", Username);    // email is the username;
		form.AddField("password", Password);
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

	public void SignUp() {
		Username = username.GetComponent<InputField> ().text;
		Password = password.GetComponent<InputField> ().text;

	}
}
