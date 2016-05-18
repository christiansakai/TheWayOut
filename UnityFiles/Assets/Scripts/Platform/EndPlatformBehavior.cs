using UnityEngine;
using System.Collections;

public class EndPlatformBehavior : MonoBehaviour {

	public string nextScene;

	void OnTriggerEnter(Collider other) {
//		DontDestroyOnLoad (GameObject.Find("GameState").transform.gameObject);
		UnityEngine.SceneManagement.SceneManager.LoadScene (nextScene);
	}
}
