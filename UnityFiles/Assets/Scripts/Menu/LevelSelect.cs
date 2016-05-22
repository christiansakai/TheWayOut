using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class LevelSelect : MonoBehaviour {

	State state;
	HighScores highScore;
	string level = "1";
	string playerid;
	bool allTime = false;
	private GameObject nameList;
	private GameObject timeList;

	void Start () {
		state = State.instance;
		playerid = state.playerid;
		nameList = GameObject.Find ("NameList");
		timeList = GameObject.Find ("TimeList");
		Filter ();
	}

	void Filter() {
		StartCoroutine (GetScores ());
	}

	public void ChangeLevel (string newLevel) {
		level = newLevel;
		Filter ();
	}

	public void DisplayPlayerScores (){
		allTime = false;
		Filter ();
	}

	public void DisplayAllScores () {
		allTime = true;
		Filter ();
	}

	public void LoadLevel() {
		state.currentLevel = level;
		state.LoadScene (level);
	}

	public void LoadMenu() {
		state.LoadScene ("Menu");
	}

	void killTheKids(GameObject parent) {
		for (int i = parent.transform.childCount - 1; i >= 0; --i) {
			Destroy (parent.transform.GetChild (i).gameObject);
		}
	}

	void PlaceScores(JSONNode list) {
		killTheKids (nameList);
		killTheKids (timeList);
		for(int i = 0; i < list.Count; i++) {
			JSONNode score = list [i];
			GameObject name = new GameObject ();
			GameObject time = new GameObject ();
			name.transform.SetParent (nameList.transform);
			time.transform.SetParent (timeList.transform);
			Text nameText = name.AddComponent<Text> ();
			Text timeText = time.AddComponent<Text> ();
			nameText.text = score["player"]["name"].Value;
			timeText.text = score["time"].Value;
			nameText.font = timeText.font = UnityEngine.Font.CreateDynamicFontFromOSFont ("Arial", 14);
			nameText.horizontalOverflow = timeText.horizontalOverflow = HorizontalWrapMode.Overflow;
			timeText.alignment = TextAnchor.UpperCenter;
		}
	}

	IEnumerator GetScores ()
	{
		string url = state.url + (allTime ? ("api/times/" + level) : ("api/users/" + playerid + "/" + level));
		using (UnityWebRequest request = UnityWebRequest.Get (url)) {
			yield return request.Send ();
			if (request.isError) {
				Debug.Log (request.error);
			} else {
				PlaceScores (JSON.Parse(request.downloadHandler.text));
			}
		}
	}
}