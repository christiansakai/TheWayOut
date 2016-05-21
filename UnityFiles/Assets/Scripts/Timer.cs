using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public float currentTime;
	public Text timer;
	
	void Update () {
		currentTime = Time.timeSinceLevelLoad;
		timer.text = string.Format ("{0:00} : {1:00}", Mathf.FloorToInt(currentTime / 60), Mathf.FloorToInt(currentTime % 60));
	}
}	