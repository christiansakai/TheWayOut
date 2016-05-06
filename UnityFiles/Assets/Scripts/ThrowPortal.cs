using UnityEngine;
using System.Collections;

public class ThrowPortal : MonoBehaviour {
	public GameObject leftPortal;
	public GameObject rightPortal;

	private Transform surface;

	GameObject mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
	}

	Vector3 currentPos;
	Vector3 lastPos;

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			throwPortal(leftPortal);
		}
		else if (Input.GetMouseButtonDown (1)) {
			throwPortal (rightPortal);
		}
		//add this var to your movment vector
		Vector3 PlatformMovement = CalculatePlatformDifference();
//		Debug.Log (currentPos);
//		transform += PlatformMovement;

	}

	Vector3 CalculatePlatformDifference() {
		if(!surface ) {
			return Vector3.zero;
		}
		return lastPos - currentPos;

	}

	void LateUpdate() {
		lastPos = currentPos;
	}

	void throwPortal(GameObject portal) {
		int x = Screen.width / 2;
		int y = Screen.height / 2;

		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			Quaternion hitObjectRotation = Quaternion.LookRotation (hit.normal);

			portal.transform.position = currentPos = hit.point;
			portal.transform.rotation = hitObjectRotation;
			surface = hit.transform;
//			portal.transform.parent = hit.transform;
		}
	}
}
