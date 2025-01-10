using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BatteryAcid.Serializables;

namespace CharacterStudio
{
    public class CSStateManager : MonoSingleton<CSStateManager>
    {
        [SerializeField] int _maxStateCount = 10;
        [SerializeField] SerializableStack<CSState> _undoStack;
        [SerializeField] SerializableStack<CSState> _redoStack;
        EventSubscription<RegisterStateArg> _registerStateSubscription;
        CSState _currentState = null;

        protected override bool Awake()
        {
            _undoStack = new Stack<CSState>();
            _redoStack = new Stack<CSState>();
            _registerStateSubscription = EventBus.Instance.Subscribe<RegisterStateArg>(OnRegisterState);
            return base.Awake();
        }

        protected override void OnDestroy()
        {
            EventBus.Instance.Unsubscribe(_registerStateSubscription);
        }

        private void OnRegisterState(RegisterStateArg arg)
        {
            AddState(arg.State);
        }

        public CSState GetUndoState()
        {
            if (_undoStack.Count == 0)
                return null;
            CSState state = new CSState(_undoStack.Pop());


            if (_currentState != null)
            {
                _redoStack.Push(_currentState);
            }
            _currentState = state;
            RemoveOldestState(_undoStack);
            return state;
        }

        public CSState GetRedoState()
        {
            if (_redoStack.Count == 0)
                return null;
            CSState state = new CSState(_redoStack.Pop());

            if (_currentState != null)
            {
                _undoStack.Push(_currentState);
            }
            _currentState = state;
            RemoveOldestState(_redoStack);
            return state;
        }

        void AddState(CSState state)
        {
            if (_currentState != null && state.Equals(_currentState))
                return;

            if (_currentState != null)
            {
                _undoStack.Push(new CSState(_currentState));
            }

            _currentState = state;
            _redoStack.Clear(); // Clear redo stack on new state addition
            RemoveOldestState(_undoStack);
        }

        void RemoveOldestState(Stack<CSState> stack)
        {
            if (stack.Count > _maxStateCount)
            {
                // Remove the oldest state
                var tempStack = new Stack<CSState>();
                while (stack.Count > 1)
                {
                    tempStack.Push(stack.Pop());
                }
                stack.Pop(); // Remove the oldest state
                while (tempStack.Count > 0)
                {
                    stack.Push(tempStack.Pop());
                }
            }
        }
    }
    public class OnRedoArg : EventArgs
    {
        public OnRedoArg() { }
    }
    public class OnUndoArg : EventArgs
    {
        public OnUndoArg() { }
    }
}
