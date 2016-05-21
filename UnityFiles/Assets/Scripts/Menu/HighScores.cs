using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class HighScores : MonoBehaviour {

	private GameObject nameList;
	private GameObject timeList;
	private JSONNode scores;
	State state;

	void Start () {
		nameList = GameObject.Find ("NameList");
		timeList = GameObject.Find ("TimeList");
		state = State.instance;
		StartCoroutine (GetScores ());
	}

	void killTheKids(GameObject parent) {
		for (int i = parent.transform.childCount - 1; i >= 0; --i) {
			Destroy (parent.transform.GetChild (i).gameObject);
		}
	}

	void placeScores(ArrayList list) {
		killTheKids (nameList);
		killTheKids (timeList);
		foreach(JSONNode score in list) {
//		for(int i = 0; i < list.Count; i++) {
			GameObject name = new GameObject ();
			GameObject time = new GameObject ();
			name.transform.SetParent (nameList.transform);
			time.transform.SetParent (timeList.transform);
			Text nameText = name.AddComponent<Text> ();
			Text timeText = time.AddComponent<Text> ();
			nameText.text = score["player"]["name"];
			timeText.text = score["time"];
			nameText.font = timeText.font = UnityEngine.Font.CreateDynamicFontFromOSFont ("Arial", 14);
			nameText.horizontalOverflow = timeText.horizontalOverflow = HorizontalWrapMode.Overflow;
			timeText.alignment = TextAnchor.UpperCenter;
		}
	}

	IEnumerator GetScores ()
	{
		using (UnityWebRequest request = UnityWebRequest.Get (state.url + "api/times")) {
			yield return request.Send ();
			if (request.isError) {
				Debug.Log (request.error);
			} else {
				scores = JSON.Parse(request.downloadHandler.text);
				FilterScores ("1", state.playerName);
			}
		}
	}

	IEnumerator GetScoresForUser ()
	{
		using (UnityWebRequest request = UnityWebRequest.Get (state.url + "api/times")) {
			yield return request.Send ();
			if (request.isError) {
				Debug.Log (request.error);
			} else {
				scores = JSON.Parse(request.downloadHandler.text);
				FilterScores ("1", state.playerName);
			}
		}
	}

	public void FilterScores(string level, string name) {
		ArrayList filteredScores = new ArrayList();
		ArrayList nameList = new ArrayList ();
		for (int i = 0; i < scores.Count; i++) {
			JSONNode score = scores [i];
			string playerName = score ["player"] ["name"].Value;
			if (score["level"]["name"].Value == level) {
				if (name == "") {
					if (!nameList.Contains (playerName)) {
						filteredScores.Add (score);
						nameList.Add (playerName);
					}
				} else if (playerName == name) {
					filteredScores.Add (score);
				}
			}
		}
		placeScores (filteredScores);
	}

}
