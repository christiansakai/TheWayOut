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

	Vector3 temp;

	Collider colliderOn;

	Vector3 prevPlatform = Vector3.zero;
	GameObject platform;

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
		rotY = !invertControls ? rotY + Input.GetAxis ("JoyStick Y") * joyStickYSensitivity : rotY - Input.GetAxis ("JoyStick Y") * joyStickYSensitivity ;
		rotY -= Input.GetAxis ("Mouse Y") * mouseSensitivity;

		transform.Rotate (0, rotX, 0);

		rotY = Mathf.Clamp (rotY, -clampLook, clampLook);
		Camera.main.transform.localRotation = Quaternion.Euler (rotY, 0, 0);

		vertical = Input.GetAxis ("Vertical") * forwardSpeed;
		horizontal = Input.GetAxis ("Horizontal") * forwardSpeed;

		// Dividing this changes orientation pulls to the right
		currentMag = characterController.velocity.magnitude;


		//got rid of isGrounded and replaced it with bool check function
		if (GroundCheck()) {
			//Debug.Log ("Euler Angles " + transform.eulerAngles);
//			Debug.Log ("Local Rotation " + transform.localRotation);
//			Debug.Log (characterController.velocity);
//			Debug.Log(vertical);
//			Debug.Log(transform.forward);
//			Debug.Log(characterController.velocity);
			// assign isJumping to whether jump button is pressed
			// change verticalVelocity based on whether isJumping is true or false
//			currXVel = vertical;
//			currZVel = horizontal;
//			Debug.Log(transform.forward);
			if (isJumping = Input.GetButtonDown ("Jump")) {
				verticalVelocity = jumpHeight;

//				xVel = currXVel > 0 ? 9 : -9;
//				zVel = currZVel > 0 ? 9 : -9;
//				if (characterController.velocity.x < 0) {
//					currXVel = -1 * characterController.velocity.x;
//				} else {
//					currXVel = characterController.velocity.x;
//				}

//				if (vertical < 0) {
//					currZVel = -1 * Mathf.Abs(characterController.velocity.z);
//				} else {
//					currZVel = Mathf.Abs(characterController.velocity.z);
//				}
//
//				if (horizontal < 0) {
//					currXVel = -1 * Mathf.Abs(characterController.velocity.x);
//				} else {
//					currXVel = Mathf.Abs(characterController.velocity.x);
//				}
//				temp = characterController.velocity;
//				temp = transform.rotation * temp;
			
				currZVel = currentMag * Mathf.Round(Input.GetAxis("Vertical"));
				currXVel = currentMag * Mathf.Round(Input.GetAxis("Horizontal"));

				if (isRunning) {
					currZVel /= 2;
					currXVel /= 2;
				}
//				Debug.Log();


//				currXVel = characterController.velocity.x;
//				currZVel = characterController.velocity.z;
//				currXVel = characterController.velocity.x;

//				currZVel = Mathf.Abs(characterController.velocity.z);

//				Debug.Log ("Transform forward: " + transform.forward);
//				Debug.Log ("Transform right: " + transform.right);
//				Debug.Log (currXVel);
			} else {
				verticalVelocity = 0;
			}
		} else {
			isRunning = false;
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
//			vertical += xVel * Time.deltaTime;
//			horizontal += zVel * Time.deltaTime;
			horizontal = horizontal / 2 + currXVel;
			vertical = vertical / 2 +  currZVel;
			//Working above
//			horizontal = currXVel;
//			vertical = currZVel;
		}

		Vector3 speed = new Vector3 (horizontal, verticalVelocity, vertical);
//		speed = transform.TransformDirection (speed);
		speed = transform.rotation * speed;
//		speed = transform.forward * speed;
//		currXVel = transform.rotation * currXVel;
//		currZVel = transform.rotation * currZVel;

		//this needs to change so player speed is adjusted if running is pressed and is in air
		if (isRunning = Input.GetButton ("Sprint") && !isJumping) {
//			Debug.Log (characterController.velocity.x);
//			horizontal = characterController.velocity.x;
//			vertical = characterController.velocity.z;
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
//			characterController.Move ((op.position + op.forward * 3));
//			transform.Rotate(0, p.rotation.eulerAngles.y + op.rotation.eulerAngles.y, 0);
			transform.rotation = Quaternion.Euler (0, p.rotation.y - transform.rotation.y + op.rotation.eulerAngles.y, 0);

			Vector3 vel = op.forward * currentMag;
			Debug.Log (vel);
//			Vector3 vel = new Vector3(op.forward.x * 20, op.forward.y * 20, op.forward.z * 20);

//			vel = transform.rotation * vel;
//			characterController.Move (vel * Time.deltaTime);
//			verticalVelocity = 0;
//			characterController.velocity.Set (vel.x, vel.y, vel.z);
//			Debug.Log (op.forward * verticalVelocity);
//			characterController.velocity.Set(vel.x, vel.y, vel.z);

//			characterController.Move (vel);

		}
	}

	private bool GroundCheck()
	{
		bool isGrounded;
		RaycastHit hitInfo;
		if (Physics.SphereCast (transform.position, characterController.radius * (1.0f - characterController.stepOffset), Vector3.down, out hitInfo,
			((characterController.height / 2f) - characterController.radius), ~0, QueryTriggerInteraction.Ignore))
		{
			colliderOn = hitInfo.collider;
			isGrounded = colliderOn.tag != "Portal";
			if (colliderOn.tag == "Platform") {
				if (platform == colliderOn.gameObject) {
					characterController.Move (colliderOn.transform.position - prevPlatform);
				}
				platform = colliderOn.gameObject;
				prevPlatform = colliderOn.transform.position;
			} else {
				platform = null;
				prevPlatform = Vector3.zero;
			}
			if (colliderOn.tag == "GravityLift") {
				isGrounded = false;
				verticalVelocity = colliderOn.gameObject.GetComponent<GravityLift> ().liftForce;
			}
		} else {
			platform = null;
			isGrounded = false;
		}
		isJumping = !isGrounded;
		return isGrounded;
	}
}
