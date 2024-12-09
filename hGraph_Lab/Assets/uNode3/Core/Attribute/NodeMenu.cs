﻿using System;
using System.Collections.Generic;

namespace MaxyGames.UNode {
	public static class NodeScope {
		public const string Default = FlowGraph;

		public const string All = nameof(All);
		public const string GameObject = nameof(GameObject);

		public const string Coroutine = nameof(Coroutine);

		public const string Macro = nameof(Macro);
		public const string StateGraph = nameof(StateGraph);
		public const string ECSGraph = nameof(ECSGraph);
		public const string FlowGraph = nameof(FlowGraph);

		public const string State = nameof(State);

		public const string Function = nameof(Function);

		public static void ApplyScopes(string scope, ICollection<string> includedScopes, ICollection<string> excludedScopes, out bool hasAllScope) {
			hasAllScope = false;
			if(scope == null) {
				scope = NodeScope.All;
				hasAllScope = true;
			}
			var scopes = scope.Split(',', '|');
			foreach(var s in scopes) {
				if(s == NodeScope.All) {
					hasAllScope = true;
					continue;
				}
				if(string.IsNullOrEmpty(s))
					continue;
				if(s[0] == '!') {
					excludedScopes.Add(s[1..]);
					continue;
				}
				includedScopes?.Add(s);
			}
		}
	}

	public abstract class BaseNodeMenuAttribute : Attribute {
		/// <summary>
		/// The menu name
		/// </summary>
		public string name;
		/// <summary>
		/// The name used for create the new node
		/// </summary>
		public string nodeName;
		/// <summary>
		/// The category of menu
		/// </summary>
		public string category;
		/// <summary>
		/// The graph scope for which the node are available for created, multiple scope can be assigned by separating with comma.
		/// </summary>
		public string scope = NodeScope.Default;
		/// <summary>
		/// The order index to sort the menu, default is 0.
		/// </summary>
		public int order { get; set; }

		/// <summary>
		/// This is auto filled.
		/// </summary>
		public Type type;

		#region Scopes
		private bool _hasAllScope;
		public bool hasAllScope {
			get {
				if(includedScopes != null) {

				}
				return _hasAllScope;
			}
		}

		private HashSet<string> _includedScopes;
		public HashSet<string> includedScopes {
			get {
				if(_includedScopes == null) {
					_includedScopes = new HashSet<string>();
					if(scope == null)
						scope = NodeScope.All;
					NodeScope.ApplyScopes(scope, _includedScopes, null, out _hasAllScope);
				}
				return _includedScopes;
			}
		}

		private HashSet<string> _excludedScopes;
		public HashSet<string> excludedScopes {
			get {
				if(_excludedScopes == null) {
					_excludedScopes = new HashSet<string>();
					if(scope == null)
						scope = NodeScope.All;
					NodeScope.ApplyScopes(scope, null, _excludedScopes, out _hasAllScope);
				}
				return _excludedScopes;
			}
		}

		public bool IsExcludedScope(string scope) {
			return excludedScopes.Contains(scope);
		}

		public bool IsValidScope(string scope) {
			if(excludedScopes.Contains(scope))
				return false;
			return _hasAllScope || includedScopes.Contains(scope);
		}

		public bool IsValidScopes(params string[] scopes) {
			for(int i=0;i< scopes.Length;i++) {
				if(!IsValidScope(scopes[i])) {
					return false;
				}
			}
			return true;
		}
		#endregion
	}

	/// <summary>
	/// Used to show menu item for Node
	/// </summary>
	[System.AttributeUsage(AttributeTargets.Class)]
	public class NodeMenu : BaseNodeMenuAttribute {
		/// <summary>
		/// The tooltip of menu
		/// </summary>
		public string tooltip;
		/// <summary>
		/// The menu icon
		/// </summary>
		public Type icon;

		public Type GetIcon() {
			if(icon == null) {
				if(hasFlowInput || hasFlowOutput) {
					return typeof(TypeIcons.FlowIcon);
				}
				if(outputs != null && outputs.Length > 0) {
					return outputs[0];
				}
				//return typeof(TypeIcons.ExtensionIcon);
			}
			return icon;
		}

		/// <summary>
		/// The list of all value inputs
		/// </summary>
		public Type[] inputs;
		/// <summary>
		/// The list of all value outputs
		/// </summary>
		public Type[] outputs;

		/// <summary>
		/// Are the node has flow input port?
		/// This will auto true when the node is inherited from <see cref="BaseFlowNode"/> or <see cref="FlowNode"/>
		/// </summary>
		public bool hasFlowInput;
		/// <summary>
		/// Are the node has flow output port?
		/// This will auto true when the node is inherited from <see cref="FlowNode"/>
		/// </summary>
		public bool hasFlowOutput;

		/// <summary>
		/// If true, this node will hide on non coroutine graph.
		/// </summary>
		public bool IsCoroutine { get; set; }

		public NodeMenu(string category, string name) {
			this.category = category;
			this.name = name;
		}

		public NodeMenu(string category, string name, Type outputType) {
			this.category = category;
			this.name = name;
			this.outputs = new[] { outputType };
		}
	}

	[System.AttributeUsage(AttributeTargets.Class)]
	public class EventMenuAttribute : BaseNodeMenuAttribute {
		public EventMenuAttribute(string category, string name) {
			this.category = category;
			this.name = name;
			this.scope = NodeScope.StateGraph;
		}
	}
}
