using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSpawner : MonoBehaviour {
	private bool triggered = false;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			if(!triggered) {
				triggered = true;
				GameController gameController = Common.GetGameController();
				gameController.GenerateTiles(transform);
			}
		}
	}
}
