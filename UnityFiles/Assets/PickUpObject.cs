using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour {
	GameObject mainCamera;
	public static bool carrying = false;
	GameObject carriedObject;
	public float distance;
	public float smooth;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (carrying) {
			//change the position of the object with camera
			carry(carriedObject);
			// if E pressed, drop the object
			checkDrop ();
		} else {
			pickup ();
		}
	}

	void carry(GameObject o){
		o.transform.position = Vector3.Lerp(o.transform.position,mainCamera.transform.position + mainCamera.transform.forward * distance,Time.deltaTime*smooth);
	}

	void pickup(){
		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log ("Pick up");
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				pickuppost p = hit.collider.GetComponent<pickuppost> ();
				if (p != null) {
					carrying = true;
					carriedObject = p.gameObject;
//					p.gameObject.GetComponent<Rigidbody>().isKinematic = true;

				}
			}
		}
	}

	void checkDrop(){
		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log ("Drop");
			dropObject ();
		}
	}

	void dropObject(){
		carrying = false;
		carriedObject = null;
//		carriedObject.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
	}
}
