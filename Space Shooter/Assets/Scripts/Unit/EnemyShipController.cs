using UnityEngine;
using System.Collections;

public class EnemyShipController : MonoBehaviour {

	public GameObject enemyShot;
	public Transform enemyShotSpawn;
	
	private Rigidbody rb;
	private Rigidbody player;

	private float differenceThreshold = 0.05f;
	private float zDifferenceThreshold = -4;
	private float speed = 4;

	private float fireRate = 1f;
	private float nextFire;
	
	void Start() {
		rb = GetComponent<Rigidbody> ();
		player = GetPlayer ();
	}

	void Update () {
		if (player == null) {
			return;
		}

		float forwardSpeed = 0;
		bool isNotWithinFiringRange = rb.position.z - player.position.z < zDifferenceThreshold;
		if(isNotWithinFiringRange) {
			forwardSpeed = speed;
		} 

		bool canShoot = Aim (forwardSpeed);
		if(canShoot && Time.time > nextFire) {
			Instantiate (enemyShot, enemyShotSpawn.position, enemyShotSpawn.rotation);
			nextFire = Time.time + fireRate;
		}

	}

	/// <summary>
	/// Attempt to put the ourselves (the enemy) behind the player, so as to kill him.
	/// </summary>
	/// <return name="args"> Whether we can now shoot the enemy.</return>
	bool Aim(float forwardSpeed) {
		Vector3 newVelocity = new Vector3(0.0f, 0.0f, forwardSpeed);
		if (PlayerShipIsRight (player, rb)) {
			if(PlayerWithinThreshold(player, rb)) {
				SetVelocity (newVelocity);
				return true;
			}

			newVelocity.x = speed;
		} else {
			if(PlayerWithinThreshold(player, rb)) {
				SetVelocity (newVelocity);
				return true;
			}

			newVelocity.x = -speed;
		}

		SetVelocity (newVelocity);
		return false;
	}

	void SetVelocity(Vector3 newVelocity) {
		rb.velocity = newVelocity;
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -4);
	}

	bool PlayerShipIsRight(Rigidbody player, Rigidbody my) {
		return my.position.x - player.position.x < 0;
	}

	bool PlayerWithinThreshold(Rigidbody player, Rigidbody my) {
		return Mathf.Abs(my.position.x - player.position.x) < differenceThreshold;
	}

	Rigidbody GetPlayer() {
		GameObject playerObject = GameObject.FindWithTag ("Player");
		if(playerObject == null) {
			Debug.Log("Cannot find object with tag 'Player'.");
		}

		Rigidbody playerBody = playerObject.GetComponent<Rigidbody> ();

		return playerBody;
	}

}

