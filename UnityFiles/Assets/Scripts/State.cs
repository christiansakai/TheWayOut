using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class State : MonoBehaviour {

	public static State instance = null;

	public string currentLevel;
	public string playerName;
	string playerEmail;
	string playerid;
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

	public void StoreUser(JSONNode user) {
		playerid = user ["_id"].Value;
		playerName = user ["name"].Value;
		playerEmail = user ["email"].Value;
		StartCoroutine (GetUserInfo ());
	}

	public void SaveUserInfo(){
		StartCoroutine (PostUserInfo ());
	}

	IEnumerator PostUserInfo(){
		WWWForm form = new WWWForm();
		form.AddField ("currentLevel", currentLevel);
		using (UnityWebRequest request = UnityWebRequest.Post (url + "api/users/" + playerid, form)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
//				Debug.Log ("updated with " + request.downloadHandler.text);
			}
		}
	}


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
				if (currentLevel == null) {
					currentLevel = "1";
					StartCoroutine (PostUserInfo ());
				}
			}
		}
	}

	public void SaveScore(float score){
		StartCoroutine (PostScore (score));
	}

	IEnumerator PostScore(float score){
		Debug.Log ("newScore! " + score);
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
				//				Debug.Log ("updated with " + request.downloadHandler.text);
			}
		}
	}

}
