using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class State : MonoBehaviour {

	public string currentLevel;
	public string playerName;
	public string playerEmail;
	public string playerid;
	public string url = "http://localhost:1337/";

	JSONNode levels;

	public void LoadScene (string scene) {
		DontDestroyOnLoad (transform.gameObject);
		Debug.Log (scene);
		SceneManager.LoadScene (scene);
		currentLevel = currentLevel + 1;
	}

	public void StoreUser(JSONNode user) {
		playerid = user ["_id"].Value;
		playerName = user ["name"].Value;
		playerEmail = user ["email"].Value;
		StartCoroutine (GetUserInfo ());
	}

	public void SaveUserInfo(){
		//save user respawn point and user current level 
		StartCoroutine(PostUserInfoWithCheckpoint());
	}

	IEnumerator PostUserInfoWithCheckpoint(){
		WWWForm form = new WWWForm();
		form.AddField ("currentLevel", currentLevel);
		form.AddField ("X", PlayerHealth.respawnPoint.x.ToString());
		form.AddField ("Y", PlayerHealth.respawnPoint.y.ToString());
		form.AddField ("Z", PlayerHealth.respawnPoint.z.ToString());
		form.AddField ("Angle", PlayerHealth.respawnPoint.z.ToString());
		using (UnityWebRequest request = UnityWebRequest.Post (url + "api/users/" + playerid, form)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
				Debug.Log ("updated with " + request.downloadHandler.text);
			}
		}
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
				Debug.Log ("updated with " + request.downloadHandler.text);
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
				// If the player has never played before, set the currentLevel to level 1;
				if (currentLevel == null) {
					currentLevel = "1";
//					StartCoroutine (PostUserInfo ());
				}
				Debug.Log (CurrentUser);
			}
		}
	}
		
}
