using System;
using System.Collections.Generic;

namespace MapaRumunii
{
    //sortowanie przez scalanie time: O(nlogn), mem: O(n)
    public class PriorityQueue<Element>
    {    
        protected Func<Element, Element, bool> LessOrGreater;
        
        public virtual void SetCompareMethod(Func<Element, Element, bool> compareMethod)
        {           
            LessOrGreater = compareMethod;            
        }
        
        private List<Element> ListOfNodes;

        public PriorityQueue()
        {
            ListOfNodes = new List<Element>();
        }

        public void Add(Element element )
        {
            ListOfNodes.Add(element);
            ListOfNodes = MergeSort(ListOfNodes);
        }

        public bool IsEmpty()
        {
            if (ListOfNodes.Count == 0) return true;
            return false;
        }

        public Element Pop()
        {
            var result = ListOfNodes[0];
            ListOfNodes.RemoveRange(0, 1);
            return result; 

        }

        private List<Element> MergeSort(List<Element> list)
        {
            if (list.Count <= 1)
                return list;

            List<Element> left = new List<Element>();
            List<Element> right = new List<Element>();

            for (int index = 0; index <= list.Count - 1; index++)
            {
                if (index < list.Count / 2)
                    left.Add(list[index]);
                else
                    right.Add(list[index]);
            }

            left = MergeSort(left);
            right = MergeSort(right);

            return Merge(left, right);
        }

        private List<Element> Merge(List<Element> left, List<Element> right)
        {
            var result = new List<Element>();

            while (left.Count != 0 && right.Count != 0)
            {
                if (LessOrGreater(left[0], right[0])) //left <= right then true; left => right then false
                {
                    result.Add(left[0]);
                    left.RemoveRange(0, 1);
                }
                else
                {
                    result.Add(right[0]);
                    right.RemoveRange(0, 1);
                }
            }

            while (left.Count != 0)
            {
                result.Add(left[0]);
                left.RemoveRange(0, 1);
            }

            while (right.Count != 0)
            {
                result.Add(right[0]);
                right.RemoveRange(0, 1);
            }

            return result;
        }
    }
}