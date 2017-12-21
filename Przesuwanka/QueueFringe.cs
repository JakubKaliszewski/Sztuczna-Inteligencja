using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przesuwanka
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