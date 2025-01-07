using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class LayoutManager : MonoSingleton<LayoutManager>
    {
        [SerializeField] private List<CSLayout> _layouts = new List<CSLayout>();
        [SerializeField] private eLayoutType _defaultLayoutType;

        private CSLayout _currentLayout;
        private CSLayout _previousLayout;
        EventSubscription<OnChangeLayoutArg> _onChangeLayoutEvent;
        EventSubscription<OnBackToPreviousLayoutArg> _onBackToPreviousLayoutEvent;
        protected override bool Awake()
        {
            foreach (var layout in _layouts)
            {
                layout.Unsetup();
            }
            _onChangeLayoutEvent = EventBus.Instance.Subscribe<OnChangeLayoutArg>(OnChangeLayout);
            _onBackToPreviousLayoutEvent = EventBus.Instance.Subscribe<OnBackToPreviousLayoutArg>(OnBackToPreviousLayout);
            return base.Awake();
        }

        private void OnBackToPreviousLayout(OnBackToPreviousLayoutArg arg)
        {
            if (_previousLayout != null)
            {
                EventBus.Instance.Publish(new OnChangeLayoutArg(_previousLayout.LayoutType));
            }
        }


        void Start()
        {
            EventBus.Instance.Publish(new OnChangeLayoutArg(_defaultLayoutType));
        }
        public void OnChangeLayout(OnChangeLayoutArg arg)
        {
            if (_currentLayout != null)
            {
                _previousLayout = _currentLayout;
                _previousLayout.Unsetup();
            }

            _currentLayout = _layouts.Find(x => x.LayoutType == arg.LayoutType);
            _currentLayout.Setup();
        }
        protected override void OnDestroy()
        {
            foreach (var layout in _layouts)
            {
                layout.Unsetup();
            }
            EventBus.Instance.Unsubscribe(_onChangeLayoutEvent);
        }
    }

    public class OnChangeLayoutArg : EventArgs
    {
        public eLayoutType LayoutType { get; private set; }
        public OnChangeLayoutArg(eLayoutType layoutType)
        {
            LayoutType = layoutType;
        }
    }
    public class OnBackToPreviousLayoutArg : EventArgs
    {
        public OnBackToPreviousLayoutArg()
        {
        }
    }
}
