using System.Collections.Generic;

namespace nHetmans
{
    class StackFringe<Element> : IFringe<Element>
    {
        private Stack<Element> stack = new Stack<Element>();

        public bool IsEmpty
        {
            get { return stack.Count == 0; }
        }

        public void Add(Element element)
        {
            stack.Push(element);
        }

        public Element Pop()
        {
            return stack.Pop();
        }
    }
}