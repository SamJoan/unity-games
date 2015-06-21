using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count = 0;
	private bool won = false;

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
		winText.text = "";
	}

	void FixedUpdate() 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 v3 = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (v3 * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText() {
		countText.text = "Count: " + count.ToString ();
		if (count == 12) {
			winText.text = "You win!";
		}
	}
}