using UnityEngine;
using System.Collections;

public class ThrowPortal : MonoBehaviour {
	public GameObject leftPortal;
	public GameObject rightPortal;

	bool leftTriggerInUse = false;
	bool rightTriggerInUse = false;
	float fireLeft;
	float fireRight;

	GameObject mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");
	}

	// Update is called once per frame
	void Update () {
		fireLeft = Input.GetAxis ("Left Fire");
		fireRight = Input.GetAxis ("Right Fire");

		if (Input.GetMouseButtonDown (0) || fireLeft == -1) {
			if (!leftTriggerInUse) {
				throwPortal (leftPortal);
				leftTriggerInUse = true;
			}
		} else {
			leftTriggerInUse = false;
		}
		if (Input.GetMouseButtonDown (1) || fireRight == -1) {
			if (!rightTriggerInUse) {
				throwPortal (rightPortal);
				rightTriggerInUse = true;
			}
		} else {
			rightTriggerInUse = false;
		}

	}
		
	void throwPortal(GameObject portal) {
		int x = Screen.width / 2;
		int y = Screen.height / 2;

		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			Quaternion hitObjectRotation = Quaternion.LookRotation (hit.normal);

			portal.transform.position = hit.point;
			portal.transform.rotation = hitObjectRotation;
			portal.transform.parent.transform.parent = hit.transform;
		}
	}
}
