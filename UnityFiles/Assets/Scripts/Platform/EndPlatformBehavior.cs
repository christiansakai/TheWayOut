using UnityEngine;
using System.Collections;

public class EndPlatformBehavior : MonoBehaviour {

	public string nextScene;
	private State state;

	void Start(){
		state = GameObject.Find ("GameState").GetComponent<State> ();
	}

	void OnTriggerEnter(Collider other) {
		state.LoadScene (nextScene);
	}
}
