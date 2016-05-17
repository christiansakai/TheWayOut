using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour {
	GameObject mainCamera;
	bool carrying;
	GameObject carriedObject;
	public float distance;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (carrying) {
			Debug.Log (carrying);
			carry(carriedObject);
			checkDrop ();
		} else {
			pickup ();
		}
	}

	void carry(GameObject o){
//		o.GetComponent<Rigidbody>().isKinematic = true;
		o.transform.position = new Vector3(1,2,0);
	}

	void pickup(){
		if (Input.GetKeyDown (KeyCode.E)) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				pickuppost p = hit.collider.GetComponent<pickuppost> ();
				if (p != null) {
					carrying = true;
					carriedObject = p.gameObject;

				}
			}
		}
	}

	void checkDrop(){
		if (Input.GetKeyDown (KeyCode.E)) {
			dropObject ();
		}
	}

	void dropObject(){
		carrying = false;
		carriedObject = null;
	}
}
