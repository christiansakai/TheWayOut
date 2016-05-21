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
	State state;
	bool allTime = false;

	// Use this for initialization
	void Start () {
		levelPanel = GameObject.Find ("LevelPanel");
		scorePanel = GameObject.Find ("ScorePanel");
		highScore = scorePanel.GetComponent<HighScores> ();
		state = State.instance;
		name = state.playerName;
	}

	public void Filter() {
		highScore.FilterScores (level, allTime ? "" : name);
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
		state.LoadScene (level);
	}

	public void LoadMenu() {
		state.LoadScene ("Menu");
	}

}
