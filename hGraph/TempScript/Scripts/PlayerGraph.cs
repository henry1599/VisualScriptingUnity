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
		MaxyGames.UNode.GraphDebug.Flow(this, 2130709941, 11, "exit");
		if(MaxyGames.UNode.GraphDebug.Value(Input.GetKeyDown(KeyCode.Space), this, 2130709941, 15, "condition", false)) {
			MaxyGames.UNode.GraphDebug.Flow(this, 2130709941, 15, "onTrue");
			MaxyGames.UNode.GraphDebug.Value(rb.velocity = Vector2.zero, this, 2130709941, 31, "target", true);
			MaxyGames.UNode.GraphDebug.Flow(this, 2130709941, 31, "exit");
			MaxyGames.UNode.GraphDebug.Value(rb, this, 2130709941, 19, "-instance", false).AddForce(MaxyGames.UNode.GraphDebug.Value(new Vector2(0F, jumpForce), this, 2130709941, 19, "-0-0", false), ForceMode2D.Impulse);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 2130709941, 19, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 2130709941, 31, true);
			MaxyGames.UNode.GraphDebug.FlowNode(this, 2130709941, 15, true);
		} else {
			MaxyGames.UNode.GraphDebug.FlowNode(this, 2130709941, 15, false);
		}
	}
}

