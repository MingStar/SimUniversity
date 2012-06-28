using System;

namespace MingStar.Utilities
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T obj)
        {
            Item = obj;
        }

        public T Item { get; private set; }
    }
}