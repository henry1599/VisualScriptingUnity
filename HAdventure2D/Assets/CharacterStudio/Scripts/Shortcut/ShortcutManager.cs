using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace CharacterStudio
{
    public class ShortcutManager : MonoSingleton<ShortcutManager>
    {
        [SerializeField] SerializedDictionary<KeyCode, ShortcutData> _shortcutsDict;
        EventSubscription<ShortcutRegisterArgs> _shortcutRegisterSubscription;
        protected override bool Awake()
        {
            _shortcutRegisterSubscription = EventBus.Instance.Subscribe<ShortcutRegisterArgs>(OnShortcutRegister);
            return base.Awake();
        }
        protected override void OnDestroy()
        {
            EventBus.Instance.Unsubscribe(_shortcutRegisterSubscription);
        }

        private void OnShortcutRegister(ShortcutRegisterArgs args)
        {
            if (_shortcutsDict == null)
            {
                _shortcutsDict = new SerializedDictionary<KeyCode, ShortcutData>();
            }
            _shortcutsDict[args.Shortcutable.Key] = args.Shortcutable;
        }


        void Update()
        {
            if (_shortcutsDict == null)
            {
                return;
            }
            foreach (var shortcut in _shortcutsDict)
            {
                if (Input.GetKeyDown(shortcut.Key))
                {
                    shortcut.Value.Callback?.Invoke();
                }
            }
        }
    }
}
