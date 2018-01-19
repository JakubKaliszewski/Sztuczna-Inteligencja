namespace Przesuwanka
{
    internal interface IFringe<Element>
    {
        bool IsEmpty { get; }
        void Add(Element element);
        Element Pop();
    }
}