using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public int scoreValue;

	private GameController gameController;

	void Start() {
		gameController = Common.GetGameController ();
	}

	void OnTriggerEnter(Collider other) {

		if (isBoundary(other)) {
			return;
		}

		if (gameObject.CompareTag ("Enemy") && other.CompareTag ("EnemyBolt")) {
			return;
		}

		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (other.gameObject);
		Destroy (gameObject);

		gameController.AddScore (scoreValue);
	}

	bool isPlayer(Collider other) {
		return other.CompareTag ("Player");
	}

	bool isBoundary(Collider other) {
		return other.CompareTag ("Boundary");
	}

}
