using System;
using System.Collections.Generic;

namespace Przesuwanka
{
    internal class QueueFringe<Element> : IFringe<Element>
    {
        private readonly Queue<Element> queue = new Queue<Element>();

        public bool IsEmpty => queue.Count == 0;

        public void Add(Element element)
        {
            queue.Enqueue(element);
        }

        public Element Pop()
        {
            return queue.Dequeue();
        }

        public void SetCompareMethod(Func<Element, Element, bool> compareMethod)
        {
            return;
        }
    }
}