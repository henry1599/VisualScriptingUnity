using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CharacterStudio
{
    /// <summary>
    /// The EventBus class manages subscriptions and event publications.
    /// </summary>
    public class EventBus : Singleton<EventBus>, IEventBus
    {
        private readonly Dictionary<Type, List<SubscriberBase>> _typedSubscribers = new Dictionary<Type, List<SubscriberBase>>();
        private readonly Dictionary<string, List<SubscriberBase>> _argsSubscribers = new Dictionary<string, List<SubscriberBase>>();
        private readonly object _lockObject = new object(); // For thread safety

        public EventBus() { }

        public void OnInitialize() { }

        public void OnPostInitialize() { }

        public void OnUnload() { }

        #region Generic Event Handling

        public EventSubscription<T> Subscribe<T>(Action<T> callback) where T : EventArgs
        {
            lock (_lockObject)
            {
                if (!_typedSubscribers.TryGetValue(typeof(T), out var subscribers))
                {
                    subscribers = new List<SubscriberBase>();
                    _typedSubscribers[typeof(T)] = subscribers;
                }

                var subscription = new EventSubscription<T>(callback);
                if (!subscribers.Contains(subscription))
                {
                    subscribers.Add(subscription);
                }
                return subscription;
            }
        }

        public void Unsubscribe<T>(EventSubscription<T> subscription) where T : EventArgs
        {
            lock (_lockObject)
            {
                if (_typedSubscribers.TryGetValue(typeof(T), out var subscribers))
                {
                    subscribers.Remove(subscription);
                }
            }
        }

        public void Publish<T>(T args) where T : EventArgs
        {
            lock (_lockObject)
            {
                if (_typedSubscribers.TryGetValue(typeof(T), out var subscribers))
                {
                    foreach (var subscriber in subscribers)
                    {
                        if (!subscriber.IsMuted)
                        {
                            if (((EventSubscription<T>)subscriber).TryGetCallback(out var callbacks))
                            {
                                callbacks.Invoke(args);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Variable Arguments Event Handling

        public EventSubscription Subscribe(Action<object[]> callback, params Type[] argTypes)
        {
            lock (_lockObject)
            {
                var key = GetKey(argTypes);

                if (!_argsSubscribers.TryGetValue(key, out var subscribers))
                {
                    subscribers = new List<SubscriberBase>();
                    _argsSubscribers[key] = subscribers;
                }

                var subscription = new EventSubscription(callback);
                if (!subscribers.Contains(subscription))
                {
                    subscribers.Add(subscription);
                }
                return subscription;
            }
        }

        public void Unsubscribe(EventSubscription subscription, params Type[] argTypes)
        {
            lock (_lockObject)
            {
                var key = GetKey(argTypes);

                if (_argsSubscribers.TryGetValue(key, out var subscribers))
                {
                    subscribers.Remove(subscription);
                }
            }
        }

        public void Publish(params object[] args)
        {
            lock (_lockObject)
            {
                var argTypes = args.Select(a => a?.GetType() ?? typeof(object)).ToArray();
                var key = GetKey(argTypes);

                if (_argsSubscribers.TryGetValue(key, out var subscribers))
                {
                    foreach (var subscriber in subscribers)
                    {
                        if (!subscriber.IsMuted)
                        {
                            if (((EventSubscription)subscriber).TryGetCallback(out var callback))
                            {
                                callback.Invoke(args);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Helper Methods and Classes

        private string GetKey(params Type[] argTypes)
        {
            return string.Join("|", argTypes.Select(type => type.FullName)); // Efficient string construction than StringBuilder
        }

        #endregion
    }
}
