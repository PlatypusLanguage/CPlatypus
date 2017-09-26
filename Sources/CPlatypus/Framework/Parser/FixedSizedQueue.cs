using System.Collections.Concurrent;

namespace CPlatypus.Framework.Parser
{
    public class FixedSizedQueue<T> : ConcurrentQueue<T>
    {
        private readonly object _syncObject = new object();

        public int Size { get; }

        public FixedSizedQueue(int size)
        {
            Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            lock (_syncObject)
            {
                while (Count > Size)
                {
                    TryDequeue(out _);
                }
            }
        }
    }
}