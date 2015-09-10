using UnityEngine;
using System.Collections;

public static class Common {
	public static GameController GetGameController() {
		GameController gameController = null;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		
		if (gameController == null) {
			Debug.LogError("Cannot find 'GameController' Script.");
		}
		
		return gameController;
	}
}