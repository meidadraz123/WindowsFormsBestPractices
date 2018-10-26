﻿using PluralsightWinFormsDemoApp.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PluralsightWinFormsDemoApp.Events
{
    class EventAggregator : IEventAggregator
    {
        public static IEventAggregator Instance { get; } = new EventAggregator();

        private readonly ConcurrentDictionary<Type, List<object>> subscriptions = new ConcurrentDictionary<Type, List<object>>();

        public void Publish<T>(T message) where T : IApplicationEvent
        {
            if (subscriptions.TryGetValue(typeof(T), out List<object> subscribers))
            {
                // To Array creates a copy in case someone unsubscribes in their own handler
                foreach (var subscriber in subscribers.ToArray())
                {
                    ((Action<T>)subscriber)(message);
                }
            }
        }

        public void Subscribe<T>(Action<T> action) where T : IApplicationEvent
        {
            var subscribers = subscriptions.GetOrAdd(typeof(T), t => new List<object>());
            lock (subscribers)
            {
                subscribers.Add(action);
            }
        }

        public void Unsubscribe<T>(Action<T> action) where T : IApplicationEvent
        {
            if (subscriptions.TryGetValue(typeof(T), out List<object> subscribers))
            {
                lock (subscribers)
                {
                    subscribers.Remove(action);
                }
            }
        }

        public void Dispose()
        {
            subscriptions.Clear();
        }
    }
}
