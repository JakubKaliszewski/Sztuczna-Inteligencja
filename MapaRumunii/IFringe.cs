using System;

namespace MapaRumunii
{
    interface IFringe<Element>
    {
        void Add(Element element);
        bool IsEmpty { get; }
        Element Pop();
        void SetCompareMethod(Func<Element, Element, bool> compareMethod);
    }
    
}
