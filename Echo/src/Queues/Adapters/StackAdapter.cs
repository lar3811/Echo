using Echo;
using Echo.Abstract;
using System.Collections.Generic;

namespace Echo.Queues.Adapters
{
    public class StackAdapter<TWave> : IWaveQueue<TWave> where TWave : IWave
    {
        private readonly Stack<TWave> _stack;

        public StackAdapter() : this(new Stack<TWave>()) { }
        public StackAdapter(Stack<TWave> stack)
        {
            _stack = stack;
        }

        public int Count => _stack.Count;

        public void Clear()
        {
            _stack.Clear();
        }

        public TWave Dequeue()
        {
            return _stack.Pop();
        }

        public void Enqueue(TWave wave)
        {
            _stack.Push(wave);
        }
    }
}
