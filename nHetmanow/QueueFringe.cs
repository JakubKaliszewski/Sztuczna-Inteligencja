using System.Collections.Generic;

namespace nHetmans
{
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