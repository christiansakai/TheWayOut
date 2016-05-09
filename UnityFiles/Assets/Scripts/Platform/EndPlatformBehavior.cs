using UnityEngine;
using System.Collections;

public class EndPlatformBehavior : MonoBehaviour {

	public string nextScene;

	void OnTriggerEnter(Collider other) {
		UnityEngine.SceneManagement.SceneManager.LoadScene (nextScene);
	}
}
