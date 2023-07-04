using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AreaType
{
    Room, 
    HallWay
}
public class MapManager : MonoBehaviour
{

    //Pertaining to Generating Map 
    public GridMapGenerator gridMapGenerator;
    public Cell[,] gridMap;
    public LayerMask wall;
    public Vector2 gridMapSize;
    public float nodeRadius;

    public float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    public int GridSizeX
    {
        get { return gridSizeX; }
        set { gridSizeX = value; }
    }
    public int GridSizeY
    {
        get { return gridSizeY; }
        set { gridSizeY = value; }
    }
    private void Start()
    {
        gridMapGenerator = GameObject.Find("MapGenerator").GetComponent<GridMapGenerator>(); // Each Scene must generate new Map aligning with the scene accordingly. 
    }

    //TODO: This is called from the path definer 
    public Cell CellFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridMapSize.x / 2) / gridMapSize.x;
        float percentY = (worldPosition.z + gridMapSize.y / 2) / gridMapSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return gridMap[x, y];
    }

    public void CheckWalkable(ref Cell start, ref Cell to)
    {
        if (start.walkable && to.walkable)
        {
            return;
        }
        else if (start.walkable && !to.walkable)
        {
            Cell newCell;
            newCell = GetShortestDistanceCell(GetNeighbours(to), start);
            if (!newCell.walkable)
            {
                CheckWalkable(ref start, ref newCell);
                return;
            }
            to = newCell;
            return; 
        }
        else if (!start.walkable && to.walkable)
        {
            Cell newCell;
            newCell = GetShortestDistanceCell(GetNeighbours(start), to);
            if (!newCell.walkable)
            {
                CheckWalkable(ref newCell, ref to);
                return;
            }
            start = newCell; 
            return;
        }
        else
        {
            Cell newStart;
            Cell newEnd;
            newEnd = GetShortestDistanceCell(GetNeighbours(to), start);
            if (!newEnd.walkable)
            {
                CheckWalkable(ref start, ref newEnd);
                return;
            }
            newStart = GetShortestDistanceCell(GetNeighbours(start), to);
            if (!newStart.walkable)
            {
                CheckWalkable(ref newStart, ref to);
                return;
            }
            start = newStart;
            to = newEnd;
            return;
        }
    }


    public Cell GetShortestDistanceCell(List<Cell> cells, Cell destinationPoint)
    {
        PriorityQueue<SoundPoint> shortestPoint = new PriorityQueue<SoundPoint>();
        Vector3 delta;
        float distance;
        foreach (Cell cell in cells)
        {
            delta = cell.worldPos - destinationPoint.worldPos;
            distance = Vector3.Dot(delta, delta);
            SoundPoint soundPoint = new SoundPoint(cell, distance); 
            shortestPoint.Enqueue(soundPoint); 
        }
        return shortestPoint.Dequeue().cell;
    }

    public (int, int) GridValueFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridMapSize.x * 0.5f) / gridMapSize.x;
        float percentY = (worldPosition.z + gridMapSize.y * 0.5f) / gridMapSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return (x, y);
    }
    public List<Cell> GetNeighbours(Cell node)
    {
        List<Cell> neighbours = new List<Cell>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(gridMap[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }
}
