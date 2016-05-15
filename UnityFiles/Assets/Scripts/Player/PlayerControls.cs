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
			verticalVelocity = 0;
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
		Debug.Log ("I'm hit!");
		if(other.collider.tag == "Portal") {
			
			Transform p = other.collider.transform;
			Transform op = other.gameObject == leftPortal ? rightPortal.transform : leftPortal.transform;
			isFalling = true;
			transform.position = op.position + op.forward * 3;
//			characterController.Move ((op.position + op.forward * 3));
//			transform.Rotate(0, p.rotation.eulerAngles.y + op.rotation.eulerAngles.y, 0);
			transform.rotation = Quaternion.Euler(0, p.rotation.y - transform.rotation.y + op.rotation.eulerAngles.y, 0);



			float mag = characterController.velocity.magnitude;
			Vector3 vel = op.forward * mag;
//			Vector3 vel = new Vector3(op.forward.x * 20, op.forward.y * 20, op.forward.z * 20);

			vel = transform.rotation * vel;
//			characterController.Move (vel * Time.deltaTime);
//			verticalVelocity = 0;
//			characterController.velocity.Set (vel.x, vel.y, vel.z);
//			Debug.Log (op.forward * verticalVelocity);
//			characterController.velocity.Set(vel.x, vel.y, vel.z);

//			characterController.Move (vel);

		}
	}

	private void GroundCheck()
	{
		previouslyGrounded = isGrounded;
		RaycastHit hitInfo;
		if (Physics.SphereCast (transform.position, characterController.radius * (1.0f - characterController.stepOffset), Vector3.down, out hitInfo,
			((characterController.height / 2f) - characterController.radius), ~0, QueryTriggerInteraction.Ignore))
		{
			isGrounded = hitInfo.collider.tag != "Portal";
		} else {
			isGrounded = false;
		}
		if (!previouslyGrounded && isGrounded && isJumping)
		{
			isJumping = false;
		}
	}
}
