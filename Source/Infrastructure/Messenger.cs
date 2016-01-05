﻿using System;
using System.Collections.Generic;

namespace UWPWorkshop.Infrastructure
{
    /// <summary>
    /// Represents an implementation of <see cref="IMessenger"/>
    /// </summary>
    public class Messenger : IMessenger
    {
        Dictionary<Type, List<WeakDelegate>> _subscribers = new Dictionary<Type, List<WeakDelegate>>();

#pragma warning disable 1591
        public void Publish<T>(T content)
        {
            var type = typeof(T);
            if (_subscribers.ContainsKey(type))
            {
                var forRemoval = new List<WeakDelegate>();

                foreach (var subscriber in _subscribers[type])
                {
                    if (subscriber.IsAlive)
                    {
                        subscriber.DynamicInvoke(content);
                    }
                    else
                    {
                        forRemoval.Add(subscriber);
                    }
                }

                forRemoval.ForEach(s => _subscribers[type].Remove(s));
            }
        }

        public void SubscribeTo<T>(Action<T> receivedCallback)
        {
            var type = typeof(T);
            List<WeakDelegate> subscribersList = null;
            if (_subscribers.ContainsKey(type))
                subscribersList = _subscribers[type];
            else
            {
                subscribersList = new List<WeakDelegate>();
                _subscribers[type] = subscribersList;
            }
            subscribersList.Add(new WeakDelegate(receivedCallback));
        }
#pragma warning restore 1591
    }
}
