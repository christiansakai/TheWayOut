using UnityEngine;
using System.Collections;

public class EndPlatformBehavior : MonoBehaviour {

	public string nextScene;
	State state;

	void Start(){
		state = GameObject.Find ("GameState").GetComponent<State> ();
	}

	void OnTriggerEnter(Collider other) {
		state.LoadScene (state.currentLevel);
	}
}
