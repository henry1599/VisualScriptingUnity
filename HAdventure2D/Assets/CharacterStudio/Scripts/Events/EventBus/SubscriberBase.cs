using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityDV.Bootstrap.EventSystem
{
    /// <summary>
    /// Interface for methods to mute and unmute the subscriber
    /// </summary>
    public interface IEventSubscriber
    {
        /// <summary>
        /// Mutes the event subscriber, preventing it from receiving events.
        /// </summary>
        void Mute();

        /// <summary>
        /// Unmutes the event subscriber, allowing it to receive events.
        /// </summary>
        void Unmute();

        /// <summary>
        /// Indicates whether the event subscriber is currently muted.
        /// </summary>
        bool IsMuted { get; }
    }


    public abstract class SubscriberBase : IEventSubscriber
    {
        public bool IsMuted { get; private set; }

        public void Mute() => IsMuted = true;

        public void Unmute() => IsMuted = false;
    }

}
