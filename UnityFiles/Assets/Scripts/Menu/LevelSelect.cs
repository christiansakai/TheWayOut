using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class LevelSelect : MonoBehaviour {

	State state;
	string level = "1";
	string playerid;
	public bool allTime = true;
	private GameObject nameList;
	private GameObject timeList;
	private Button allTimeButton;
	private Button playerButton;
	private Image allTimeImage;
	private Image playerImage;
	private Button levelButton;
	private Text levelName;

	void Start () {
		state = State.instance;
		playerid = state.playerid;
		nameList = GameObject.Find ("NameList");
		timeList = GameObject.Find ("TimeList");
		allTimeButton = GameObject.Find ("AllTimeBest").GetComponent<Button>();
		playerButton = GameObject.Find ("PlayerBest").GetComponent<Button>();
		allTimeImage = allTimeButton.GetComponent<Image> ();
		playerImage = playerButton.GetComponent<Image> ();
		levelButton = GameObject.Find ("1").GetComponent<Button> ();
		levelName = GameObject.Find ("LevelName").GetComponent<Text> ();
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		DisplayAllScores ();
	}

	void Filter() {
		levelName.text = levelButton.GetComponentsInChildren<Text> ()[0].text.ToUpper ();
		levelButton.GetComponent<Image>().color = levelButton.colors.pressedColor;
		StartCoroutine (GetScores ());
	}

	public void ChangeLevel (string newLevel) {
		levelButton.GetComponent<Image> ().color = levelButton.colors.normalColor;
		levelButton = GameObject.Find (newLevel).GetComponent<Button> ();
		level = newLevel;
		Filter ();
	}

	public void DisplayPlayerScores (){
		allTime = false;
		allTimeImage.color = allTimeButton.colors.normalColor;
		playerImage.color = playerButton.colors.pressedColor;
		Filter ();
	}

	public void DisplayAllScores () {
		allTime = true;
		allTimeImage.color = allTimeButton.colors.pressedColor;
		playerImage.color = playerButton.colors.normalColor;
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
			nameText.font = timeText.font = UnityEngine.Font.CreateDynamicFontFromOSFont ("Arial", 18);
			nameText.horizontalOverflow = timeText.horizontalOverflow = HorizontalWrapMode.Overflow;
			nameText.verticalOverflow = timeText.verticalOverflow = VerticalWrapMode.Overflow;
			timeText.alignment = TextAnchor.UpperRight;
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
				JSONNode scores = JSON.Parse (request.downloadHandler.text);
				PlaceScores (scores);
			}
		}
	}
}