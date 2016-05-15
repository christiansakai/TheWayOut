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
	public float runMultipler = 5.0f;
	public bool isRunning = false;

	private bool isJumping = false;
	private bool isFalling = true;

	private bool isGrounded = false;
	private bool previouslyGrounded = false;
	Vector3 GroundContactNormal;

	float rotY = 0;
	float rotX;
	float joyRotY;
	float joyRotX;
	float vertical;
	float horizontal;
	float verticalVelocity = 0;

	CharacterController characterController;

	GameObject leftPortal;
	GameObject rightPortal;

	// Use this for initialization
	void Start () {
		leftPortal = GameObject.Find ("LeftPortal");
		rightPortal = GameObject.Find ("RightPortal");
		characterController = GetComponent<CharacterController> ();
		Cursor.lockState = CursorLockMode.Locked;
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

		GroundCheck ();
		if (!isJumping && !isGrounded && !isFalling) {
			isFalling = true;
			verticalVelocity = 0.5f;
		} else if (isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpHeight;
			isJumping = true;
		} else if (isGrounded) {
			isFalling = false;
		}


		Vector3 speed = new Vector3 (horizontal, verticalVelocity, vertical);

		speed = transform.rotation * speed;

		if (isGrounded && Input.GetButton ("Sprint")) {
			speed = speed * runMultipler;
			isRunning = true;
		} else {
			isRunning = false;
		}

		characterController.Move (speed * Time.deltaTime);
	}

	void OnControllerColliderHit(ControllerColliderHit other) {
		if(other.collider.tag == "Portal") {
			Transform p = other.collider.transform;
			Transform op = other.gameObject == leftPortal ? rightPortal.transform : leftPortal.transform;
			isFalling = true;
			transform.position = op.position + op.forward * 3;
			transform.Rotate(0, p.rotation.eulerAngles.y + op.rotation.eulerAngles.y, 0);

//			Debug.Log (op.forward * verticalVelocity);

		}
	}

	private void GroundCheck()
	{
		previouslyGrounded = isGrounded;
		RaycastHit hitInfo;
		isGrounded = (Physics.SphereCast (transform.position, characterController.radius * (1.0f - characterController.stepOffset), Vector3.down, out hitInfo,
			((characterController.height / 2f) - characterController.radius), ~0, QueryTriggerInteraction.Ignore));
		if (!previouslyGrounded && isGrounded && isJumping)
		{
			isJumping = false;
		}
	}
}
