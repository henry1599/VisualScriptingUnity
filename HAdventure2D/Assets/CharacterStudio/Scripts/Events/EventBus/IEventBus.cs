using System;
using UnityEngine;

namespace CharacterStudio
{
    /// <summary>
    /// Interface for the EventBus, which handles event subscriptions, unsubscriptions, and publications.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Subscribes to an event of type <typeparamref name="T"/>, where <typeparamref name="T"/> is a subclass of <see cref="EventArgs"/>.
        /// </summary>
        /// <typeparam name="T">The type of event, which must be derived from <see cref="EventArgs"/>.</typeparam>
        /// <param name="callback">The callback to be invoked when the event is published.</param>
        /// <returns>An event subscription that can be used to unsubscribe later.</returns>
        EventSubscription<T> Subscribe<T>(Action<T> callback) where T : EventArgs;

        /// <summary>
        /// Unsubscribes from an event of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of event, which must be derived from <see cref="EventArgs"/>.</typeparam>
        /// <param name="subscription">The subscription instance returned by <see cref="Subscribe{T}"/>.</param>
        void Unsubscribe<T>(EventSubscription<T> subscription) where T : EventArgs;

        /// <summary>
        /// Publishes an event of type <typeparamref name="T"/> to all subscribers.
        /// </summary>
        /// <typeparam name="T">The type of event, which must be derived from <see cref="EventArgs"/>.</typeparam>
        /// <param name="args">The event arguments to be passed to the subscribers.</param>
        void Publish<T>(T args) where T : EventArgs;

        /// <summary>
        /// Subscribes to an event with a set of arguments, using an array of object parameters.
        /// </summary>
        /// <param name="callback">The callback to be invoked when the event with matching argument types is published.</param>
        /// <param name="argTypes">The expected argument types for this event subscription.</param>
        /// <returns>An event subscription that can be used to unsubscribe later.</returns>
        EventSubscription Subscribe(Action<object[]> callback, params Type[] argTypes);

        /// <summary>
        /// Unsubscribes from an event with specific argument types.
        /// </summary>
        /// <param name="subscription">The subscription instance returned by <see cref="Subscribe(Action{object[]}, Type[])"/>.</param>
        /// <param name="argTypes">The argument types that were used when subscribing.</param>
        void Unsubscribe(EventSubscription subscription, params Type[] argTypes);

        /// <summary>
        /// Publishes an event with variable arguments.
        /// </summary>
        /// <param name="args">The arguments for the event.</param>
        void Publish(params object[] args);
    }
}
