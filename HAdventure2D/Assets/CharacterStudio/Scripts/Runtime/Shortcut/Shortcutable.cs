using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterStudio
{
    public class Shortcutable : MonoBehaviour
    {
        public ShortcutData Data;
        void Start()
        {
            RegisterShortcut();
        }
        private void RegisterShortcut()
        {
            EventBus.Instance?.Publish(new ShortcutRegisterArgs(Data));
        }
    }
    public class ShortcutRegisterArgs : EventArgs
    {
        public ShortcutData Shortcutable;
        public ShortcutRegisterArgs(ShortcutData shortcutable)
        {
            Shortcutable = shortcutable;
        }
    }

    [Serializable]
    public class ShortcutData
    {
        public KeyCode Key;
        public UnityEvent Callback;
    }
}
