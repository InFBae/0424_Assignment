using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructure
{
    internal class PriorityQueue<TElement, TPriority>
    {        
        private struct Node
        {
            public TElement element;
            public TPriority priority;
        }
        private List<Node> nodes;
        private IComparer<TPriority> comparer;
        public int Count { get { return nodes.Count; } }

        public PriorityQueue()      // 기본 생성자
        { 
            this.nodes = new List<Node>();
            this.comparer = Comparer<TPriority>.Default;
        }

        public PriorityQueue(IComparer<TPriority> comparer)     // comparer를 입력할 경우 그 comparer로 비교
        {
            this.nodes = new List<Node>();
            this.comparer = comparer;
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            Node newNode = new Node() { element = element, priority = priority };

            nodes.Add(newNode);
            int newIndex = nodes.Count - 1;
            while (newIndex > 0) 
            {
                int parentNodeIndex = GetParentIndex(newIndex);
                // 새 노드의 우선순위가 더 낮을 경우 스왑
                if (comparer.Compare(nodes[newIndex].priority, nodes[parentNodeIndex].priority) < 0)
                {
                    nodes[newIndex] = nodes[parentNodeIndex];
                    newIndex = parentNodeIndex;
                    nodes[newIndex] = newNode;
                }
                else
                {
                    break;
                }
            }
        }

        public TElement Dequeue()
        {
            if (nodes.Count == 0)
                throw new InvalidOperationException();

            Node rootNode = nodes[0];
            Node lastNode = nodes[nodes.Count - 1];
            nodes[0] = lastNode;
            nodes.RemoveAt(Count - 1);
            int index = 0;

            while (index < nodes.Count) 
            { 
                int leftChildIndex = GetLeftChildIndex(index);
                int rightChildIndex = GetRightChildIndex(index);

                // 자식이 둘 다 있을 때
                if(rightChildIndex < nodes.Count)
                {
                    // 왼쪽 자식의 우선순위가 더 낮으면 왼쪽 아니라면 오른쪽
                    int lessChildIndex = comparer.Compare(nodes[leftChildIndex].priority, nodes[rightChildIndex].priority) < 0 ? leftChildIndex : rightChildIndex;
                    // 비교된 자식의 우선순위가 더 낮다면 스왑
                    if (comparer.Compare(nodes[lessChildIndex].priority, nodes[index].priority) < 0)
                    {
                        nodes[index] = nodes[lessChildIndex];
                        index = lessChildIndex;
                    }
                    else
                    {
                        nodes[index] = lastNode;
                        break;
                    }
                }
                // 왼쪽 자식만 있을 때
                else if (leftChildIndex < nodes.Count) 
                {
                    // 왼쪽 자식의 우선순위가 더 낮을경우 스왑
                    if (comparer.Compare(nodes[leftChildIndex].priority, nodes[index].priority) < 0)
                    {
                        nodes[index] = nodes[leftChildIndex];
                        index = leftChildIndex;
                    }
                    else
                    {
                        nodes[index] = lastNode;
                        break;
                    }
                }
                // 자식이 하나도 없을 때
                else
                {
                    nodes[index] = lastNode;
                    break;
                }
            }
            return rootNode.element;
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }
        private int GetLeftChildIndex(int index)
        {
            return index * 2 + 1;
        }
        private int GetRightChildIndex(int index)
        {
            return index * 2 + 2;
        }
    }



}
