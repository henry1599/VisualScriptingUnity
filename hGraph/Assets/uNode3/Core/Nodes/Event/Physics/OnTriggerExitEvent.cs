﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace MaxyGames.UNode.Nodes {
    [EventMenu("Physics", "On Trigger Exit")]
	[StateEvent]
	[Description("OnTriggerExit is called when the Collider other has stopped touching the trigger.")]
	public class OnTriggerExitEvent : BaseComponentEvent {
		public ValueOutput value { get; set; }

		protected override void OnRegister() {
			base.OnRegister();
			value = ValueOutput<Collider>(nameof(value));
		}

		public override void OnRuntimeInitialize(GraphInstance instance) {
			base.OnRuntimeInitialize(instance);
			if(instance.target is Component comp) {
				UEvent.Register(UEventID.OnTriggerExit, comp, (Collider val) => {
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
			DoGenerateCode(UEventID.OnTriggerExit, new[] { value });
		}
	}
}