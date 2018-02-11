using System;

namespace MapaRumunii
{
    public class PriorityQueueFringe<Element> : IFringe<Element>
    {
        private PriorityQueue<Element> priorityQueue = new PriorityQueue<Element>();
        
        public void Add(Element element)
        {
            priorityQueue.Add(element);
        }

        public bool IsEmpty
        {
            get { return priorityQueue.IsEmpty(); }
        }

        public Element Pop()
        {
            return priorityQueue.Pop();
        }

        public virtual void SetCompareMethod(Func<Element, Element, bool> compareMethod)
        {
            priorityQueue.SetCompareMethod(compareMethod);
        }
    }
}