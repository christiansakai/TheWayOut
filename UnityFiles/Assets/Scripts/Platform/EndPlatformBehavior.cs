using UnityEngine;
using System.Collections;

public class EndPlatformBehavior : MonoBehaviour {

	public string nextScene;
	private State state;

	void Start(){
		state = State.instance;
	}

	void OnTriggerEnter(Collider other) {
		state.SaveScore (Time.timeSinceLevelLoad);
		state.currentLevel = nextScene;
		state.SaveUserInfo ();
		state.LoadScene (nextScene);
	}
}
