namespace MapaRumunii
{
    public class PriorityQueueFringe<State> : IFringe<Node<State>>
    {
        private PriorityQueue<State> priorityQueue = new PriorityQueue<State>();
        
        public void Add(Node<State> element)
        {
            priorityQueue.Add(element);
        }

        public bool IsEmpty { get; }

        public Node<State> Pop()
        {
            return priorityQueue.Pop();
        }
    }
}