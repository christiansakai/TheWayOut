using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HighScores : MonoBehaviour {

	private ArrayList scores = new ArrayList();
	private GameObject nameList;
	private GameObject timeList;

	void Start () {

		nameList = GameObject.Find ("NameList");
		timeList = GameObject.Find ("TimeList");
		
		Score score1 = new Score ();
		Score score2 = new Score ();
		Score score3 = new Score ();
		Score score4 = new Score ();
		Score score5 = new Score ();
		Score score6 = new Score ();

		score1.name = "Bill Higgins";
		score1.time = 12345;

		score2.name = "Rudger Light";
		score2.time = 85948;

		score3.name = "Jo Blow";
		score3.time = 9999;

		score4.name = "Cindy Bindy";
		score4.time = 12345;

		score5.name = "Immad Facade";
		score5.time = 12345;

		score6.name = "Ari Bobarry";
		score6.time = 12345;

		scores.Add (score1);
		scores.Add (score2);
		scores.Add (score3);
		scores.Add (score4);
		scores.Add (score5);
		scores.Add (score6);

		placeScores (scores);
	}

	void killTheKids(GameObject parent) {
		for (int i = parent.transform.childCount - 1; i >= 0; --i) {
			Destroy (parent.transform.GetChild (i).gameObject);
		}
	}

	void placeScores(ArrayList list) {
		killTheKids (nameList);
		killTheKids (timeList);
		foreach (Score score in list) {
			GameObject name = new GameObject ();
			GameObject time = new GameObject ();
			name.transform.SetParent (nameList.transform);
			time.transform.SetParent (timeList.transform);
			Text nameText = name.AddComponent<Text> ();
			Text timeText = time.AddComponent<Text> ();
			nameText.text = name.name = score.name.ToString ();
			timeText.text = time.name = score.time.ToString ();
			nameText.font = timeText.font = UnityEngine.Font.CreateDynamicFontFromOSFont ("Arial", 14);
			nameText.horizontalOverflow = timeText.horizontalOverflow = HorizontalWrapMode.Overflow;
			timeText.alignment = TextAnchor.UpperCenter;
		}
	}

}

public class Score {
	public string name;
	public int time;
}