﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace MaxyGames.UNode.Nodes {
    [EventMenu("Physics", "On Collision Stay")]
	[StateEvent]
	[Description("OnCollisionStay is called once per frame for every Collider or Rigidbody that touches another Collider or Rigidbody.")]
	public class OnCollisionStayEvent : BaseComponentEvent {
		public ValueOutput value { get; set; }

		protected override void OnRegister() {
			base.OnRegister();
			value = ValueOutput<Collision>(nameof(value));
		}

		public override void OnRuntimeInitialize(GraphInstance instance) {
			base.OnRuntimeInitialize(instance);
			if(instance.target is Component comp) {
				UEvent.Register(UEventID.OnCollisionStay, comp, (Collision val) => {
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
			DoGenerateCode(UEventID.OnCollisionStay, new[] { value });
		}
	}
}