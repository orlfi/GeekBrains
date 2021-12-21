using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace FileCommander.ViewModels
{
    public class MessageBus
    {
        private static MessageBus _instance;
        public static MessageBus Instance => _instance ??= new MessageBus();

        public delegate void BusMessageHandler(object value);

        private Dictionary<string, BusMessageHandler> _subscribers;

        public MessageBus()
        {
            _subscribers = new Dictionary<string, BusMessageHandler>();
        }

        public void Subscribe(string EventName, BusMessageHandler message)
        {
            if (_subscribers.ContainsKey(EventName))
            {
                _subscribers[EventName] += message;
            }
            else
            {
                _subscribers.Add(EventName, message);
            }
        }

        public void Send(string eventName, object value)
        {
            if (_subscribers.ContainsKey(eventName))
            {
                _subscribers[eventName].Invoke(value);
            }
        }
    }
}