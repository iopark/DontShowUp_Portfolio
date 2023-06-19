using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : IComparable<Cell>
{
    public int gridX; 
    public int gridY;
    public Vector3 worldPos;
    public bool walkable; 

    int fCost;
    int hCost; 
    public int gCost
    {
        get { return fCost + hCost; }
    }

    public Cell ()
    public int CompareTo(Cell nodeToCompare)
    {
        //총합의 fCost 가더 작은
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            // if hCost of this is smaller than the comparing one, 
            // this would result in -1. // The result needs to be -1, if priority is higher 
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return compare;
    }
}
