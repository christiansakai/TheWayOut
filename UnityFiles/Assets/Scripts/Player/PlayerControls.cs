using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public float forwardSpeed = 5.0f;
	public float strafeSpeed = 4.0f;
	public float mouseSensitivity = 2.0f;
	public float clampLook = 86.0f;
	public float jumpHeight = 7.0f;
	public bool invertControls = false;
	public float joyStickXSensitivity = 2.0f;
	public float joyStickYSensitivity = 2.0f;
	public float runMultipler = 2.0f;
	public bool isRunning = false;

	float rotY = 0;
	float rotX;
	float joyRotY;
	float joyRotX;
	float vertical;
	float horizontal;
	float verticalVelocity = 0;

	CharacterController characterController;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {

		//Rotation
		rotX = Input.GetAxis ("JoyStick X") * joyStickXSensitivity;
		rotX += Input.GetAxis("Mouse X") * mouseSensitivity;
		rotY += Input.GetAxis ("JoyStick Y") * joyStickYSensitivity;
		rotY -= Input.GetAxis ("Mouse Y") * mouseSensitivity;


		transform.Rotate (0, rotX, 0);

		rotY = Mathf.Clamp (rotY, -clampLook, clampLook);
		Camera.main.transform.localRotation = Quaternion.Euler (rotY, 0, 0);
		
		//Movement
		vertical = Input.GetAxis ("Vertical") * forwardSpeed;
		horizontal = Input.GetAxis ("Horizontal") * strafeSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		 
		if (characterController.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpHeight;
		}

		Vector3 speed = new Vector3 (horizontal, verticalVelocity, vertical);

//		Debug.Log (verticalVelocity);
		speed = transform.rotation * speed;

		if (characterController.isGrounded && Input.GetButton ("Sprint")) {
			speed = speed * runMultipler;
			isRunning = true;
		} else {
			isRunning = false;
		}

		characterController.Move (speed * Time.deltaTime);
	}

	void OnControllerColliderHit(ControllerColliderHit other) {
		if(other.collider.tag == "Portal") {
			GameObject portal = other.collider.GetComponent<StepThroughPortal> ().otherPortal;
			transform.position = portal.transform.position + portal.transform.forward * 3;
		}
	}
}
