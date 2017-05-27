using Echo.Abstract;
using System.Collections.Generic;

namespace Echo.QueueAdapters
{
    public class StackAdapter : IEchoQueue
    {
        private readonly Stack<Echo> _stack;

        public StackAdapter() : this(new Stack<Echo>()) { }
        public StackAdapter(Stack<Echo> stack)
        {
            _stack = stack;
        }

        public int Count => _stack.Count;

        public void Clear()
        {
            _stack.Clear();
        }

        public Echo Dequeue()
        {
            return _stack.Pop();
        }

        public void Enqueue(Echo echo)
        {
            _stack.Push(echo);
        }
    }
}
