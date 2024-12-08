///--------------------------------------------///
///-----MADE WITH: UNODE VISUAL SCRIPTING-----///
///------------------------------------------///
#pragma warning disable
using UnityEngine;
using System.Collections.Generic;

public class PipeGraph : MaxyGames.UNode.RuntimeBehaviour {	
	public float speed;
	public float endX;
	public Rigidbody2D rb;
	public bool isSetup;
	
	private void Start() {
	}
	
	private void Update() {
		if(isSetup) {
			CheckLifeTime();
		}
	}
	
	public void Setup(float pipeSpeed, float pipeEndX) {
		speed = pipeSpeed;
		endX = pipeEndX;
		isSetup = true;
		rb.velocity = new Vector2((0F - speed), 0F);
	}
	
	public void CheckLifeTime() {
		if((this.transform.position.x < endX)) {
			Object.Destroy(this.gameObject);
		}
	}
}

