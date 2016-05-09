using UnityEngine;
using System.Collections;

public class EndPlatformBehavior : MonoBehaviour {

	public string nextScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		UnityEngine.SceneManagement.SceneManager.LoadScene (nextScene);
	}
}
