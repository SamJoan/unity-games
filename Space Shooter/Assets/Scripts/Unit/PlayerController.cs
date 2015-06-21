using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public Boundary boundary;
	public float tilt;

	public GameObject shot;
	public Transform shotSpawn;
	public GameObject playerExplosion;
	private Rigidbody rb;

	public float fireRate = 0.5f;
	private float nextFire;

	private GameController gameController;
	
	void Start() {
		rb = GetComponent<Rigidbody> ();
		gameController = Common.GetGameController ();
	}

	void Update() {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) { 
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 v3 = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = v3 * speed;

		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Boundary")) {
			return;
		}

		Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
		Destroy (other.gameObject);
		Destroy (gameObject);

		gameController.GameOver ();
	}
}
