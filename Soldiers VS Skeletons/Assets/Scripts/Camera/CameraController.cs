using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public Transform target;
	
	void FixedUpdate() {
		float distance = 2.75f;
		Vector3 position = target.position + (-target.forward * distance) + new Vector3(0f, 2.25f, 0f);
		transform.position = position;
		
		var relativePos = (target.position + (target.forward * 3f)) - transform.position;
		var rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;
	}
}
