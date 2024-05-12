using System;

namespace DCTools
{
    public interface IEventHandler
    {
        public void Subscribe<T>(Action<T> action) where T : class, ITinyMessage;
        public void Unsubscribe<T>();
        public void Publish(ITinyMessage message);
    }
}