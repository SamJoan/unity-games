using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public Transform target;
	Vector3 offset;

	void Start()
	{
		offset = transform.position - target.position;
	}

	void FixedUpdate() {
		float distance = 2f;
		Vector3 position = target.position + (-target.forward * distance) + new Vector3(0f, 1.5f, 0f);
		transform.position = position;
		
		transform.LookAt(target);
	}
}
