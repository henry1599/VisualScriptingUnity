///--------------------------------------------///
///-----MADE WITH: UNODE VISUAL SCRIPTING-----///
///------------------------------------------///
#pragma warning disable
using UnityEngine;
using System.Collections.Generic;

public class PlayerGraph : MaxyGames.UNode.RuntimeBehaviour {	
	public float jumpForce;
	public Rigidbody2D rb;
	
	private void Start() {
	}
	
	private void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			rb.velocity = new Vector2(rb.velocity.x, 0F);
			rb.AddForce(new Vector2(0F, jumpForce), ForceMode2D.Impulse);
		}
	}
}

