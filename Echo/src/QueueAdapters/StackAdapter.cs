using Echo.Abstract;
using System.Collections.Generic;

namespace Echo.QueueAdapters
{
    public class StackAdapter : IEchoQueue
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

        public void Enqueue(Wave echo)
        {
            _stack.Push(echo);
        }
    }
}
