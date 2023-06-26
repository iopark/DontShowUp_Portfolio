using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
