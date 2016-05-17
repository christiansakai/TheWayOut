using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;

public class Login : MonoBehaviour {
	public GameObject username;
	public GameObject password;
	private string Username;
	private string Password;

	// Use this for initialization
	void Start () {
	
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
		SceneManager.LoadScene ("Menu");
	}

	public class UserData
	{
		public string user;
		public static UserData CreateFromJson(string jsonString)
		{
			return JsonUtility.FromJson<UserData> (jsonString);
		}
	}

	IEnumerator UserAuthentification(string username, string password)
	{
		WWWForm form = new WWWForm();
		form.AddField("email", username);    // email is the username;
		form.AddField("password", password);
		using (UnityWebRequest request = UnityWebRequest.Post ("http://localhost:1337/login", form)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
				UserData CurrentUser = UserData.CreateFromJson (request.downloadHandler.text);
				LogInEnter ();
			}
		}
	}
}
