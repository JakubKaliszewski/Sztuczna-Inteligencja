using System.Collections.Generic;

namespace MapaRumunii
{
    class  queue <State> : IFringe<Node<State>>
    {
        public void Add(Node<State> element)
        {
            throw new System.NotImplementedException();
        }

        public bool IsEmpty { get; }
        public Node<State> Pop()
        {
            throw new System.NotImplementedException();
        }
    }
    
    class QueueFringe<Element> : IFringe<Element>
    {
        private Queue<Element> queue = new Queue<Element>();

        public bool IsEmpty
        {
            get { return queue.Count == 0; }
        }

        public void Add(Element element)
        {
            queue.Enqueue(element);
        }

        public Element Pop()
        {
            return queue.Dequeue();
        }
    }
}