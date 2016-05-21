using UnityEngine;
using System.Collections;

public class OnGameStart : MonoBehaviour {
	public GameObject gameState;

	void Awake(){
		if (State.instance == null) {
			Instantiate (gameState);
		}
	}
}