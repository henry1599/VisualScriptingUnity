using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDV.Bootstrap.EventSystem
{
    /// <summary>
    /// Represents a subscription to a generic event.
    /// </summary>
    public class EventSubscription<T> : SubscriberBase where T : EventArgs
    {
        private readonly Action<T> _weakCallback;

        public EventSubscription(Action<T> callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            _weakCallback = new Action<T>(callback);
        }

        public bool TryGetCallback(out Action<T> callback)
        {
            callback = _weakCallback;
            return callback != null;
        }
    }

    /// <summary>
    /// Represents a subscription to an event with variable arguments.
    /// </summary>
    public class EventSubscription : SubscriberBase
    {
        private readonly WeakReference<Action<object[]>> _weakCallback;

        public EventSubscription(Action<object[]> callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            _weakCallback = new WeakReference<Action<object[]>>(callback);
        }

        public bool TryGetCallback(out Action<object[]> callback)
        {
            return _weakCallback.TryGetTarget(out callback);
        }
    }

}