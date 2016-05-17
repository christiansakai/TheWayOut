using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Experimental.Networking;
using SimpleJSON;

public class LevelSelect : MonoBehaviour {

	JSONNode levels;
	GameObject levelPanel;
	GameObject scorePanel;
	HighScores highScore;
	string level = "1";
	string name = "";

	// Use this for initialization
	void Start () {
		levelPanel = GameObject.Find ("LevelPanel");
		scorePanel = GameObject.Find ("ScorePanel");
		highScore = scorePanel.GetComponent<HighScores> ();
	}

	public void Filter() {
		highScore.FilterScores (level, name);
	}

	public void ChangeLevel (string newLevel) {
		level = newLevel;
		Filter ();
	}

	public void ChangeName (string newName) {
		name = newName;
		Filter ();
	}

}
