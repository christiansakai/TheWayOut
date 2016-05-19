using UnityEngine;
using System.Collections;

public class PostOnCollision : MonoBehaviour {
	public GameObject LeftGate;
	public GameObject RightGate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Post2") {
			LeftGate.transform.Translate (Vector3.forward * 3);
			RightGate.transform.Translate(Vector3.forward * (-3));
		}

	}
}
