﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace MaxyGames.UNode.Nodes {
    [EventMenu("Physics", "On Trigger Stay")]
	[StateEvent]
	[Description("OnTriggerStay is called once per physics update for every Collider other that is touching the trigger.")]
	public class OnTriggerStayEvent : BaseComponentEvent {
		public ValueOutput value { get; set; }

		protected override void OnRegister() {
			base.OnRegister();
			value = ValueOutput<Collider>(nameof(value));
		}

		public override void OnRuntimeInitialize(GraphInstance instance) {
			base.OnRuntimeInitialize(instance);
			if(instance.target is Component comp) {
				UEvent.Register(UEventID.OnTriggerStay, comp, (Collider val) => {
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
			DoGenerateCode(UEventID.OnTriggerStay, new[] { value });
		}
	}
}