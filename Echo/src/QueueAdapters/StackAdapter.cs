using Echo;
using Echo.Abstract;
using System.Collections.Generic;

namespace wave.QueueAdapters
{
    public class StackAdapter : IWaveQueue
    {
        private readonly Stack<Wave> _stack;

        public StackAdapter() : this(new Stack<Wave>()) { }
        public StackAdapter(Stack<Wave> stack)
        {
            _stack = stack;
        }

        public int Count => _stack.Count;

        public void Clear()
        {
            _stack.Clear();
        }

        public Wave Dequeue()
        {
            return _stack.Pop();
        }

        public void Enqueue(Wave wave)
        {
            _stack.Push(wave);
        }
    }
}
