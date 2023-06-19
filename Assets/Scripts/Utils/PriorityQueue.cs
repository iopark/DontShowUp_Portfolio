using System;
using System.Collections.Generic;
using UnityEngine; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PriorityQueue<TElement> where TElement : IComparable<TElement>
{
    private List<TElement> cells;

    public PriorityQueue()
    {
        this.cells = new List<TElement>();
    }


    public int Count { get { return cells.Count; } }

    public void Enqueue(TElement element)
    {
        PushHeap(element);
    }

    public TElement Peek()
    {
        if (cells.Count == 0)
            throw new InvalidOperationException();

        return cells[0];
    }

    public bool TryPeek(out TElement element)
    {
        if (cells.Count == 0)
        {
            element = default(TElement);
            return false;
        }

        element = cells[0];
        return true;
    }

    public TElement Dequeue()
    {
        if (cells.Count == 0)
            throw new InvalidOperationException();

        TElement rootNode = cells[0];
        PopHeap();
        return rootNode;
    }

    public bool TryDequeue(out TElement element)
    {
        if (cells.Count == 0)
        {
            element = default(TElement);
            return false;
        }
        TElement rootNode = cells[0];
        element = rootNode; 
        PopHeap();
        return true;
    }

    private void PushHeap(TElement newCell)
    {
        cells.Add(newCell);
        int newCellIndex = cells.Count - 1;
        while (newCellIndex > 0)
        {
            int parentIndex = GetParentIndex(newCellIndex);
            TElement parentNode = cells[parentIndex];

            // if newCell priority is higher than the parent, 
            if (newCell.CompareTo(parentNode) < 0)
            {
                cells[newCellIndex] = parentNode; //swap parent to the newCell's index 
                newCellIndex = parentIndex;
            }
            else
            {
                break;
            }
        }
        cells[newCellIndex] = newCell;
    }

    private void PopHeap()
    {
        TElement lastNode = cells[cells.Count - 1];
        cells.RemoveAt(cells.Count - 1);

        int index = 0;
        while (index < cells.Count)
        {
            int leftChildIndex = GetLeftChildIndex(index);
            int rightChildIndex = GetRightChildIndex(index);

            if (rightChildIndex < cells.Count)
            {
                int compareIndex = cells[leftChildIndex].CompareTo(cells[rightChildIndex]) < 0 ?
                leftChildIndex : rightChildIndex;

                if (cells[compareIndex].CompareTo(lastNode) < 0)
                {
                    cells[index] = cells[compareIndex];
                    index = compareIndex;
                }
                else
                {
                    cells[index] = lastNode;
                    break;
                }
            }
            else if (leftChildIndex < cells.Count)
            {
                if (cells[leftChildIndex].CompareTo(lastNode) < 0)
                {
                    cells[index] = cells[leftChildIndex];
                    index = leftChildIndex;
                }
                else
                {
                    cells[index] = lastNode;
                    break;
                }
            }
            else
            {
                cells[index] = lastNode;
                break;
            }
        }
    }

    private int GetParentIndex(int childIndex)
    {
        return (childIndex - 1) / 2;
    }

    private int GetLeftChildIndex(int parentIndex)
    {
        return parentIndex * 2 + 1;
    }

    private int GetRightChildIndex(int parentIndex)
    {
        return parentIndex * 2 + 2;
    }
}

