using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public float startingTime = 0;
	private float currentTime;
	public Text timer;

	// Use this for initialization
	void Awake () {
		currentTime = startingTime;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		float minutes = currentTime / 60;
		float seconds = currentTime % 60;

		timer.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
	}
}	