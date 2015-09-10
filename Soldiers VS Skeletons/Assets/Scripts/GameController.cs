using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GameObject tile;
	private int callTimes = 0;
	
	private List<Vector3> tileVectors = new List<Vector3>() {
		new Vector3(1f, 0f, 0f),
		new Vector3(1f, 0f, 1f),
		new Vector3(0f, 0f, 1f),
		new Vector3(-1f, 0f, 1f),
		new Vector3(-1f, 0f, 0f),
		new Vector3(-1f, 0f, -1f),
		new Vector3(0f, 0f, -1f),
		new Vector3(1f, 0f, -1f)
	};
	
	public void GenerateTiles(Transform triggeredTile) {
		if(callTimes > 300) {
			return;
		}
		
		callTimes += 1;
		
		foreach(Vector3 v in tileVectors) {
			Vector3 spawnPosition = triggeredTile.position + v;
			Quaternion spawnRotation = triggeredTile.rotation;
			
			if(!ExistsAtPosition(spawnPosition)) {
				Instantiate(tile, spawnPosition, spawnRotation);
			}
		}		
	}
	
	bool ExistsAtPosition(Vector3 position) {
		//Vector3 spawnPos = (whatever position you want to check)
		float radius = 0.1f;
		if (Physics.CheckSphere (position, radius)) {
			return true;
		} else {
			return false;
		}
	}
}