using System;
using System.Collections.Generic;
using System.Linq;

namespace DCTools
{
    public class EventHandler : IEventHandler, IDisposable
    {
        private readonly Dictionary<Type, TinyMessageSubscriptionToken> _tokens;

        public EventHandler()
        {
            _tokens = new Dictionary<Type, TinyMessageSubscriptionToken>();
        }
        
        public void Subscribe<T>(Action<T> action) where T : class, ITinyMessage
        {
            if (!BaseLocator.DoesServiceExist(typeof(ITinyMessengerHub)))
            {
                BaseLocator.Add<ITinyMessengerHub>(new TinyMessengerHub());
            }
            _tokens.Add(typeof(T), BaseLocator.Find<ITinyMessengerHub>().Subscribe(action));
        }

        public void Unsubscribe<T>()
        {
            var type = typeof(T);
            foreach (var token in _tokens.Where(token => token.Key == type))
            {
                BaseLocator.Find<ITinyMessengerHub>().Unsubscribe(token.Value);
                _tokens.Remove(token.Key);
                break;
            }
        }

        public void Publish(ITinyMessage message)
        {
            BaseLocator.Find<ITinyMessengerHub>().Publish(message);
        }
        
        public void Dispose()
        {
            if (_tokens.Count <= 0) return;
            
            foreach (var token in _tokens)
            {
                BaseLocator.Find<ITinyMessengerHub>().Unsubscribe(token.Value);
            }
            _tokens.Clear();
        }

        public Dictionary<Type, TinyMessageSubscriptionToken> GetTokens() => _tokens;
    }
}