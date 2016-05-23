using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class State : MonoBehaviour {

	public static State instance = null;

	public string currentLevel;
	public JSONNode respawnPoint;
	public string playerName;
	string playerEmail;
	public string playerid;
	public string url = "http://localhost:1337/";

	JSONNode levels;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void LoadScene (string scene) {
		SceneManager.LoadScene (scene);
	}

	// get user info from login 
	public void StoreUser(JSONNode user) {
		playerid = user ["_id"].Value;
		playerName = user ["name"].Value;
		playerEmail = user ["email"].Value;
		StartCoroutine (GetUserInfo ());
	}

	public Vector3 GetRespawnPoint(){
		return new Vector3 (float.Parse (respawnPoint ["X"].Value), float.Parse (respawnPoint ["Y"].Value), float.Parse (respawnPoint ["Z"].Value));
	}
	public Vector3 GetRespawnAngle(){
		return new Vector3 (0, float.Parse (respawnPoint ["Angle"].Value));
	}

	public void ResetRespawn(){
		SetRespawn (Vector3.zero, 0f);
	}

	public void SetRespawn(Vector3 newPoint, float angle){
		respawnPoint ["X"].Value = newPoint.x.ToString();
		respawnPoint ["Y"].Value = newPoint.y.ToString();
		respawnPoint ["Z"].Value = newPoint.z.ToString();
		respawnPoint ["Angle"].Value = angle.ToString();
	}

	// save user information when exiting to the main menu from the game
	public void SaveUserInfo(){
		StartCoroutine (PostUserInfoWithCheckpoint ());
	}

	IEnumerator PostUserInfoWithCheckpoint(){
		WWWForm form = new WWWForm();
		form.AddField ("currentLevel", currentLevel);
		form.AddField ("X", respawnPoint["X"].Value);
		form.AddField ("Y", respawnPoint["Y"].Value);
		form.AddField ("Z", respawnPoint["Z"].Value);
		form.AddField ("Angle", respawnPoint["Angle"].Value);
		using (UnityWebRequest request = UnityWebRequest.Post (url + "api/users/" + playerid, form)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
				StartCoroutine (GetUserInfo ());
			}
		}
	}
		
	// Get user info when loaded to menu from the login;
	IEnumerator GetUserInfo()
	{
		using (UnityWebRequest request = UnityWebRequest.Get (url + "api/users/" + playerid)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
				JSONNode CurrentUser = JSON.Parse(request.downloadHandler.text);
				currentLevel = CurrentUser ["currentLevel"] ["name"].Value;
				respawnPoint = CurrentUser ["respawnPoint"];
				if (currentLevel == "") {
					currentLevel = "1";
				}
			}
		}
	}
		
	public void SaveScore(float score){
		StartCoroutine (PostScore (score));
	}

	IEnumerator PostScore(float score){
		WWWForm form = new WWWForm();
		form.AddField ("level", currentLevel);
		form.AddField ("time", score.ToString());
		form.AddField ("player", playerid);
		using (UnityWebRequest request = UnityWebRequest.Post (url + "api/times/", form)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
			
			}
		}
	}

	public void Login(string user, string pw){
		StartCoroutine(UserAuthentification(user, pw));
	}

	IEnumerator UserAuthentification(string user, string pw)
	{
		WWWForm form = new WWWForm();
		form.AddField("email", user);
		form.AddField("password", pw);
		using (UnityWebRequest request = UnityWebRequest.Post (url + "login", form)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
				JSONNode CurrentUser = JSON.Parse(request.downloadHandler.text);
				StoreUser (CurrentUser ["user"]);
				LoadScene ("Menu");
			}
		}
	}
		
}
