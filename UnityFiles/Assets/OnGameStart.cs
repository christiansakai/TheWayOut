using UnityEngine;
using System.Collections;

public class OnGameStart : MonoBehaviour {
	public GameObject gameState;
	// Use this for initialization
	void Awake(){
		if (State.instance == null) {
			Instantiate (gameState);
		}
	}
}