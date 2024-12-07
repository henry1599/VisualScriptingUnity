///--------------------------------------------///
///-----MADE WITH: UNODE VISUAL SCRIPTING-----///
///------------------------------------------///
#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class PipeSpawner : MaxyGames.UNode.RuntimeBehaviour {	
	public PipeGraph pipePrefab;
	public float spawnInterval;
	[ShowInInspector]
	private float spawnCounter;
	public float spawnStartX;
	public Vector2 spawnBoundY;
	public float endX;
	public float pipeSpeed;
	
	private void Start() {
		spawnCounter = spawnInterval;
	}
	
	private void Update() {
		if((spawnCounter > 0F)) {
			spawnCounter = (spawnCounter - Time.deltaTime);
			return;
		}
		spawnCounter = spawnInterval;
		Spawn();
	}
	
	public void Spawn() {
		Vector2 spawnPos = Vector2.zero;
		PipeGraph pipeInstance = default(PipeGraph);
		spawnPos = new Vector2(spawnStartX, Random.Range(spawnBoundY.x, spawnBoundY.y));
		pipeInstance = (Object.Instantiate(pipePrefab, spawnPos, new Quaternion(), this.transform) as PipeGraph);
		pipeInstance.Setup(spawnPos, endX, pipeSpeed);
	}
}

