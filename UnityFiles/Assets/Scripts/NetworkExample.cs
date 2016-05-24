using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Experimental.Networking;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public Text networkText;

	private Rigidbody rb;
	private float startTime;
	private int count;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		winText.text = "";
		startTime = Time.time;
		SetCountText ();
		StartCoroutine(GetPlayerCount ());
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
//		Destroy (other.gameObject);
		if (other.gameObject.CompareTag ("collectible")) {
			other.gameObject.SetActive (false);
			count += 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 8) {
			winText.text = "YOU WIN!!";
			StartCoroutine(PushScore (Time.time - startTime));
		}
	}

	void SetNetworkText(string count, string best)
	{
		networkText.text = "You are player #" + count + ". The time to beat is: " + best + " seconds";
	}

	public class UrlData
	{
		public float bestTime;
		public int count;

		public static UrlData CreateFromJSON(string jsonString)
		{
			return JsonUtility.FromJson<UrlData>(jsonString);
		}
	}


	IEnumerator GetPlayerCount ()
	{
		using (UnityWebRequest request = UnityWebRequest.Get ("http://localhost:1337/")) {
			yield return request.Send ();

			if (request.isError) { // Error
				Debug.Log (request.error);
			} else { // Success
				UrlData test = UrlData.CreateFromJSON(request.downloadHandler.text);
				SetNetworkText (test.count.ToString(), test.bestTime.ToString());
			}
		}
	}

	IEnumerator PushScore(float score)
	{
		Debug.Log (score);
		WWWForm form = new WWWForm();
		form.AddField("score", score.ToString());
		using (UnityWebRequest request = UnityWebRequest.Post ("http://localhost:1337/", form)) {
			yield return request.Send();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
			}
		}
	}

}
