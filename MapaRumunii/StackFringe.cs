using System;
using System.Collections.Generic;

namespace MapaRumunii
{
    internal class StackFringe<Element> : IFringe<Element>
    {
        private readonly Stack<Element> stack = new Stack<Element>();

        public bool IsEmpty => stack.Count == 0;

        public void Add(Element element)
        {
            stack.Push(element);
        }

        public Element Pop()
        {
            return stack.Pop();
        }

        public void SetCompareMethod(Func<Element, Element, bool> compareMethod)
        {
        }
    }
}