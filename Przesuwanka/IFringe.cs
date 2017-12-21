using System;
using System.Collections.Generic;

namespace Przesuwanka
{
    interface IFringe<Element>
    {
        void Add(Element element);
        bool IsEmpty { get; }
        Element Pop();
    }
}