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

    public Cell parent; 
    public int gCost;
    public int hCost; 
    public int fCost
    {
        get { return gCost + hCost; }
        set { fCost =  value; }
    }

    public Cell (int _gridX, int _gridY, Vector3 pos, bool walkable)
    {
        this.gridX = _gridX;
        this.gridY = _gridY;
        this.worldPos = pos;
        this.walkable = walkable;
    }

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
