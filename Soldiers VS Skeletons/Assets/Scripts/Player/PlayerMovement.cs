using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed = 6f;
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	Actions characterActions;

	float timer = 0f;
	float rotation = 0f; 

	void Awake()
	{
		playerRigidbody = GetComponent<Rigidbody> ();
		characterActions = GetComponent<Actions> ();
	}

	void FixedUpdate()
	{
		timer += Time.deltaTime;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		float mouseX = Input.GetAxis("Mouse X");
		
		Move (h, v, timer);
		Rotate(mouseX);
	}

	void Move (float h, float v, float timer)
	{
		bool moving = h != 0 || v != 0;
		if (moving) {		
			movement = transform.forward * v;
			playerRigidbody.AddForce (movement);
			characterActions.Walk ();
		} else {
			characterActions.Stay ();
		}
	}
	
	void Rotate(float mouseX) {
		rotation += mouseX;
		transform.rotation = Quaternion.Euler(0f, rotation, 0f);
	}

}
