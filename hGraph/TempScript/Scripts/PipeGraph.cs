///--------------------------------------------///
///-----MADE WITH: UNODE VISUAL SCRIPTING-----///
///------------------------------------------///
#pragma warning disable
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class PipeGraph : MaxyGames.UNode.RuntimeBehaviour {	
	[SerializeField]
	[ReadOnly]
	private float speed;
	public Rigidbody2D rb;
	[SerializeField]
	[ReadOnly]
	private Vector2 startPos;
	[SerializeField]
	[ReadOnly]
	private float endX;
	[SerializeField]
	[ReadOnly]
	private bool isSetup;
	
	private void Start() {
	}
	
	private void Update() {
		if(isSetup) {
			if((this.transform.position.x <= endX)) {
				Object.Destroy(base.gameObject);
			}
		}
	}
	
	public void Setup(Vector2 startPos, float endX, float pipeSpeed) {
		pipeSpeed = speed;
		endX = endX;
		startPos = startPos;
		rb.velocity = new Vector2((0F - speed), 0F);
		isSetup = true;
	}
}

