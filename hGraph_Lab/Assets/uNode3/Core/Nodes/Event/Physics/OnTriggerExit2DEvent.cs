﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace MaxyGames.UNode.Nodes {
    [EventMenu("Physics", "On Trigger Exit 2D")]
	[StateEvent]
	[Description("Sent when another object leaves a trigger collider attached to this object (2D physics only).")]
	public class OnTriggerExit2DEvent : BaseComponentEvent {
		public ValueOutput value { get; set; }

		protected override void OnRegister() {
			base.OnRegister();
			value = ValueOutput<Collider2D>(nameof(value));
		}

		public override void OnRuntimeInitialize(GraphInstance instance) {
			base.OnRuntimeInitialize(instance);
			if(instance.target is Component comp) {
				UEvent.Register(UEventID.OnTriggerExit2D, comp, (Collider2D val) => {
					instance.defaultFlow.SetPortData(value, val);
					Trigger(instance);
				});
			} else {
				throw new Exception("Invalid target: " + instance.target + "\nThe target type must inherit from `UnityEngine.Component`");
			}
		}

		public override void OnGeneratorInitialize() {
			var variable = CG.RegisterVariable(value);
			CG.RegisterPort(value, () => variable);
		}

		public override void GenerateEventCode() {
			DoGenerateCode(UEventID.OnTriggerExit2D, new[] { value });
		}
	}
}