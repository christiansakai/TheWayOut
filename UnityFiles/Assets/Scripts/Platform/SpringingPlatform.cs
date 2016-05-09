using UnityEngine;
using System.Collections;

public class SpringingPlatform : MonoBehaviour {

	//alternate way to make variable public - acts differently?
	[SerializeField]
	Transform platform;

	[SerializeField]
	Transform startTransform;

	[SerializeField]
	Transform endTransform;

	[SerializeField]
	float platformSpeed;

	Vector3 direction;
	Transform destination;

	void Start(){
		SetDestination (startTransform);
	}

	void FixedUpdate() {
		float timeSpeed = platformSpeed * Time.fixedDeltaTime;
//		platform.GetComponent<Rigidbody>().MovePosition (platform.position + direction * timeSpeed);
		// alternative using Lerp - which moves from one position to another.
		// much jerkier, but would be interesting as a different mechanic
		platform.position = Vector3.Slerp(platform.position, destination.position, timeSpeed);

		if (Vector3.Distance (platform.position, destination.position) < timeSpeed) {
			SetDestination (destination == startTransform ? endTransform : startTransform);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube (startTransform.position, platform.localScale);

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (endTransform.position, platform.localScale);
	}

	void SetDestination(Transform dest) {
		destination = dest;
		direction = (destination.position - platform.position).normalized;
	}

}
