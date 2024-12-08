///--------------------------------------------///
///-----MADE WITH: UNODE VISUAL SCRIPTING-----///
///------------------------------------------///
#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using MaxyGames.UNode;

public class PipeSpawner : RuntimeBehaviour {	
	public float spawnInterval;
	public float spawnCounter;
	public float pipeSpeed;
	public float endX;
	public float startX;
	public Vector2 boundY;
	public GameObject pipePrefab;
	
	private void Start() {
		spawnCounter = spawnInterval;
	}
	
	private void Update() {
		if((spawnCounter > 0F)) {
			spawnCounter = (spawnCounter - Time.deltaTime);
			return;
		}
		 else {
			spawnCounter = spawnInterval;
			Spawn();
		}
	}
	
	public void Spawn() {
		Vector2 spawnPos = Vector2.zero;
		GameObject pipeObjectInstance = default(GameObject);
		GameObject pipeInstance = default(GameObject);
		spawnPos = GetSpawnPos();
		pipeInstance = Object.Instantiate<UnityEngine.GameObject>(pipePrefab, spawnPos, new Quaternion(), this.transform);
		pipeInstance.GetGeneratedComponent<PipeGraph>().Setup(pipeSpeed, endX);
	}
	
	public Vector2 GetSpawnPos() {
		return new Vector2(startX, Random.Range(boundY.x, boundY.y));
	}
}

