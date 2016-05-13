using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HighScores : MonoBehaviour {

	public UILineInfo HighScoreTable;
	HighScores highScores;
	public ArrayList scores = new ArrayList();

	void Start () {
		
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

	string addScore (string name, int time, DateTime date) {
		return name + time.ToString () + date.ToString ();
	}

	void placeScores(IList list) {
		foreach (Score score in list) {
			GameObject ngo = new GameObject();
			ngo.AddComponent<LayoutElement> ();
			LayoutElement le = ngo.GetComponent<LayoutElement> ();
			le.preferredHeight = 35.125f;
			le.preferredWidth = 1f;
			ngo.transform.SetParent(this.transform);

			Text myText = ngo.AddComponent<Text>();
			myText.horizontalOverflow = HorizontalWrapMode.Overflow;
			myText.font = UnityEngine.Font.CreateDynamicFontFromOSFont ("Arial", 14);
			myText.text = score.name.ToString() + "  " + score.time.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class Score {
	public string name;
	public int time;
}