using System;

namespace nHetmans
{
    public interface IFringe<Element>
    {
        bool IsEmpty { get; }
        void Add(Element element);
        Element Pop();
        void SetCompareMethod(Func<Element, Element, bool> compareMethod);
    }
}