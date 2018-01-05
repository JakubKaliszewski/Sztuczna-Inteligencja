namespace MapaRumuniiOdleglosciLiniaProsta
{
    interface IFringe<Element>
    {
        void Add(Element element);
        bool IsEmpty { get; }
        Element Pop();
    }
}