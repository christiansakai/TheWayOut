using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float joySensitivity = 5.0f;
	public float upDownRange = 60.0f;
	public float jumpSpeed = 7.0f;

	float verticalRotation = 0;
	float verticalVelocity = 0;
	float leftRight;
	float prevAxis;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController cc = GetComponent<CharacterController> ();
		
		//Player Rotation
		leftRight = Input.GetAxis("JoyStick X") * joySensitivity;
		leftRight += Input.GetAxis("Mouse X") * mouseSensitivity;
		Debug.Log (leftRight);
		transform.Rotate (0, leftRight, 0);

		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation += Input.GetAxis("JoyStick Y") * joySensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		//Player Movement

		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;


		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		if (cc.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpSpeed;
		}

		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);

		speed = transform.rotation * speed;


		GetComponent<Rigidbody> ().velocity = speed;

		cc.Move (speed * Time.deltaTime);
	}
}
