using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridMapGenerator : MonoBehaviour
{
    public bool debug; 
    public LayerMask wall;
    public Vector2 gridMapSize;
    public float nodeRadius;
    private Cell[,] debugMap; 

    float nodeDiameter;
    public int gridSizeX;
    public int gridSizeY;

    public int GridSizeX
    {
        get { return gridSizeX; }
    }
    public int GridSizeY
    {
        get { return gridSizeY; }
    }

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2; 
        gridSizeX = Mathf.RoundToInt(gridMapSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridMapSize.y / nodeDiameter);
        CopyToManager(); 
        CreateGrid(); 
    }

    public void GenerateGrid()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridMapSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridMapSize.y / nodeDiameter);
        CopyToManager();
        CreateGrid();
    }

    private void CopyToManager()
    {
        GameManager.MapManager.wall = wall;
        GameManager.MapManager.nodeRadius = nodeRadius;
        GameManager.MapManager.nodeDiameter = nodeRadius * 2;
        GameManager.MapManager.GridSizeX = gridSizeX;
        GameManager.MapManager.GridSizeY = gridSizeY;
        GameManager.MapManager.gridMapSize = gridMapSize;
    }
    private void CreateGrid()
    {
        debugMap = new Cell[gridSizeX, gridSizeY]; 
        GameManager.MapManager.gridMap = new Cell[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridMapSize.x / 2 - Vector3.forward * gridMapSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, wall));
                GameManager.MapManager.gridMap[x, y] = new Cell(x, y, worldPoint, walkable);
                debugMap[x, y] = new Cell(x, y, worldPoint, walkable); 
            }
            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridMapSize.x, 1, gridMapSize.y));
        if (!debug)
        {
            foreach (Cell cell in debugMap)
            {
                Gizmos.color = (cell.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(cell.worldPos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
