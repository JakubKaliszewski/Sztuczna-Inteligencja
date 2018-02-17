using System;

namespace MapaRumunii
{
    internal interface IFringe<Element>
    {
        bool IsEmpty { get; }
        void Add(Element element);
        Element Pop();
        void SetCompareMethod(Func<Element, Element, bool> compareMethod);
    }
}