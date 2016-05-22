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
	public bool isJumping = false;
	public bool isGrounded = false;

	float rotY = 0;
	float rotX;
	float joyRotY;
	float joyRotX;
	float vertical;
	float horizontal;
	float verticalVelocity = 0;
	float currXVel;
	float currZVel;
	float currentMag;
	GameObject templight;

	bool isPortaling = false;
	bool isVertical = false;

	Vector3 portalVel;

	Collider colliderOn;

	Vector3 prevPlatform = Vector3.zero;
	GameObject platform;

	CharacterController characterController;

	GameObject leftPortal;
	GameObject rightPortal;



	// Use this for initialization
	void Start () {
//		templight = GameObject.FindGameObjectWithTag ("templight");
		leftPortal = GameObject.Find ("LeftPortal");
		rightPortal = GameObject.Find ("RightPortal");
		characterController = GetComponent<CharacterController> ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		//Rotation
		rotX = Input.GetAxis ("JoyStick X") * joyStickXSensitivity;
		rotX += Input.GetAxis("Mouse X") * mouseSensitivity;
		rotY = !invertControls ? rotY + Input.GetAxis ("JoyStick Y") * joyStickYSensitivity : rotY - Input.GetAxis ("JoyStick Y") * joyStickYSensitivity ;
		rotY -= Input.GetAxis ("Mouse Y") * mouseSensitivity;

		transform.Rotate (0, rotX, 0);

		rotY = Mathf.Clamp (rotY, -clampLook, clampLook);
		Camera.main.transform.localRotation = Quaternion.Euler (rotY, 0, 0);
//		templight.transform.localRotation = Quaternion.Euler (rotY, 0, 0);

		vertical = Input.GetAxis ("Vertical") * forwardSpeed;
		horizontal = Input.GetAxis ("Horizontal") * forwardSpeed;

		// Dividing this changes orientation pulls to the right
		currentMag = characterController.velocity.magnitude;


		//got rid of isGrounded and replaced it with bool check function
		if (GroundCheck()) {
			portalVel = Vector3.zero;
			isPortaling = false;
			isVertical = false;
			if (isJumping = Input.GetButtonDown ("Jump")) {
				verticalVelocity = jumpHeight;
			
				currZVel = currentMag * Mathf.Round(Input.GetAxis("Vertical"));
				currXVel = currentMag * Mathf.Round(Input.GetAxis("Horizontal"));

				if (isRunning) {
					currZVel /= 2;
					currXVel /= 2;
				}

			} 

			else {
				verticalVelocity = 0;
			}
		} 
		else if (isPortaling) {
				verticalVelocity += Physics.gravity.y * Time.deltaTime;

				if (!isVertical) {
					horizontal = portalVel.x;
					vertical = portalVel.z;
				}
			} 
		else {
			isRunning = false;
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
			horizontal = horizontal / 2 + currXVel;
			vertical = vertical / 2 +  currZVel;

		}
		Vector3 speed = new Vector3 (horizontal, verticalVelocity, vertical);
		speed = transform.rotation * speed;

		if (isRunning = Input.GetButton ("Sprint") && !isJumping) {
			speed *= isJumping ? runMultipler / 2 : runMultipler;
		}

		characterController.Move (speed * Time.deltaTime);
	}

	void OnControllerColliderHit(ControllerColliderHit other) {
		if (other.collider.tag == "Portal") {
			
			Transform p = other.collider.transform;
			Transform op = other.gameObject == leftPortal ? rightPortal.transform : leftPortal.transform;
			isJumping = true;
			transform.position = op.position + op.forward * 3;
			transform.rotation = Quaternion.Euler (0, p.rotation.y - transform.rotation.y + op.rotation.eulerAngles.y, 0);

			if(op.transform.rotation.eulerAngles.z == 0 && op.transform.rotation.eulerAngles.y == 0) {
				portalVel = new Vector3(0,characterController.velocity.y, 0);
				isPortaling = true;
				isVertical = true;

			}
			else {
				isVertical = false;
				portalVel = new Vector3(0,0,currentMag);
				isPortaling = true;
			}
		}
	}

	private bool GroundCheck()
	{
		Vector3 shift = Vector3.zero;
		RaycastHit hitInfo;
		if (Physics.SphereCast (transform.position, characterController.radius * (1.0f - characterController.stepOffset), Vector3.down, out hitInfo,
			((characterController.height / 2f) - characterController.radius), ~0, QueryTriggerInteraction.Ignore))
		{
			colliderOn = hitInfo.collider;
			isGrounded = colliderOn.tag != "Portal";
			shift.y = (characterController.height / 2f - characterController.radius) - hitInfo.distance;
			if (colliderOn.tag == "GravityLift") {
				isGrounded = false;
				verticalVelocity = colliderOn.gameObject.GetComponent<GravityLift> ().liftForce;
			}

			if (colliderOn.tag == "Platform") {
				if (platform == colliderOn.gameObject) {
					characterController.Move (colliderOn.transform.position - prevPlatform);
				} else {
					characterController.Move (shift);
				}
				isGrounded = true;
				platform = colliderOn.gameObject;
				prevPlatform = colliderOn.transform.position;
			} else if (isGrounded) {
				platform = null;
				prevPlatform = Vector3.zero;
				characterController.Move (shift);
			}

		} else {
			platform = null;
			isGrounded = false;
		}
		isJumping = !isGrounded;
		return isGrounded;
	}
}
