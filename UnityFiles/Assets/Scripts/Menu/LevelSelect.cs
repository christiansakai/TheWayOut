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

	// Use this for initialization
	void Start () {
		levelPanel = GameObject.Find ("LevelPanel");
		scorePanel = GameObject.Find ("ScorePanel");
		highScore = scorePanel.GetComponent<HighScores> ();
		state = GameObject.Find ("GameState").GetComponent<State> ();
	}

	public void Filter(string scores) {
		highScore.FilterScores (level, scores);
	}

	public void ChangeLevel (string newLevel) {
		level = newLevel;
		Filter ("");
	}

	public void ChangeName (string newName) {
		name = newName;
		Filter (name);
	}

	public void DisplayPlayerScores (){
		Filter (name);
	}

	public void DisplayAllScores () {
		Filter ("");
	}

	public void LoadLevel() {
		string toLoad = "";
		if (level == "1") {
			toLoad = "GetTrough";
		} else if (level == "2") {
			toLoad = "PlatformerInTheSky";
		} else if (level == "3") {
			toLoad = "CindyLevel";
		}
		state.LoadScene (toLoad);
	}

	public void LoadMenu() {
		state.LoadScene ("Menu");
	}

}
