using UnityEngine;
using System.Collections;

// https://www.assetstore.unity3d.com/en/#!/content/18220
public class PlayerMovement : MonoBehaviour
{
	public float forwardSpeed = 6f;
	public float backwardSpeed = 3f;
	Animator anim;
	Rigidbody playerRigidbody;
	
	float timer = 0f;
	float rotation = 0f; 

	void Awake()
	{
		playerRigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate() {
		timer += Time.deltaTime;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		float mouseX = Input.GetAxis("Mouse X");
		
		Move (h, v, timer);
		Rotate(mouseX);
	}

	void Move (float h, float v, float timer) {
		if(v != 0 || h != 0) {
			Vector3 merged = GetMovementVector(h, v);
			playerRigidbody.MovePosition (transform.position + merged);
		}
		
		Animate(h, v);
	}
	
	Vector3 GetMovementVector(float h, float v) {
		Vector3 forwardMovement = new Vector3();
		if(v != 0) {
			forwardMovement = transform.forward * v;
		}
		
		Vector3 lateralMovement = new Vector3();
		if(h != 0) {
			lateralMovement = transform.right * h;
		}

		float walkSpeed;
		if(v > 0 && h != 0) {
			walkSpeed = forwardSpeed;
		} else {
			walkSpeed = backwardSpeed;
		}
		
		Vector3 merged = (forwardMovement + lateralMovement);
		Debug.Log(merged);
		Debug.Log(merged.normalized);
		merged = merged.normalized * walkSpeed * Time.deltaTime;
			
		return merged;
	}
	
	void Animate(float h, float v) {
		bool moving = h != 0 || v != 0;
		if(moving) {
			if(v != 0) {
				if(v > 0) {
					anim.SetBool("WalkingForwards", true);
				} else {
					anim.SetBool("WalkingBackwards", true);
				}
			} else {
				Stop(h, v);
			}
			
			if(h != 0) {
				if(h > 0) {
					anim.SetBool("WalkingRight", true);
				} else {
					anim.SetBool("WalkingLeft", true);
				}
			} else {
				Stop(h, v);
			}
			
		} else {
			Stop(h, v);
		}
	}
	
	void Stop(float h, float v) {
		if(v == 0) {
			anim.SetBool("WalkingForwards", false);
			anim.SetBool("WalkingBackwards", false);
		}
		
		if(h == 0) {
			anim.SetBool("WalkingLeft", false);
			anim.SetBool("WalkingRight", false);
		}
	}
	
	void Rotate(float mouseX) {
		rotation += mouseX;
		transform.rotation = Quaternion.Euler(0f, rotation, 0f);
	}

}
