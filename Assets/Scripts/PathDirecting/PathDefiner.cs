using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PathDefiner: MonoBehaviour
{
    public bool[,] visited; 
    const int CostStraight = 10;
    const int CostDiagonal = 14;

    private int xSize; 
    private int ySize;
    public Cell[,] gridMap; 
    private void Start()
    {
        
        //TODO: Define the bool map based on the x and y value 
    }

    /// <summary>
    /// Would this be under Monster's point of View? 
    /// Then, the startPoint is the Monter's Point of View 
    /// endPoint is where the sound was heard. 
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    public void StartDefiningPath(Vector3 startPoint, Vector3 endPoint)
    {
        StartCoroutine(FindPath(startPoint, endPoint)); 
    }

    IEnumerator FindPath(Vector3 startPoint, Vector3 endPoint)
    {
        int xSize = GameManager.MapManager.GridSizeX;
        int ySize = GameManager.MapManager.GridSizeY;
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Cell startSpot = GameManager.MapManager.CellFromWorldPoint(startPoint);
        Cell targetSpot = GameManager.MapManager.CellFromWorldPoint(endPoint);


        if (startSpot.walkable && targetSpot.walkable)
        {
            PriorityQueue<Cell> openSet = new PriorityQueue<Cell>();
            bool[,] visited = new bool[xSize, ySize]; 
            openSet.Enqueue(startSpot);

            while (openSet.Count > 0)
            {
                Cell currentSpot = openSet.Dequeue();
                visited[currentSpot.gridX, currentSpot.gridY] = true;
                if (currentSpot == targetSpot)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Cell neighbour in GameManager.MapManager.GetNeighbours(currentSpot))
                {
                    if (!neighbour.walkable || visited[neighbour.gridX, neighbour.gridY] == true)
                    {
                        continue;
                    }
                    int g = currentSpot.gCost + ((currentSpot.gridX == neighbour.gridX ||
                        currentSpot.gridY == neighbour.gridY) ? CostStraight : CostDiagonal);
                    int h = Heuristic(neighbour, targetSpot);

                    int newPotentialCost = g + h; 
                    if (newPotentialCost < neighbour.fCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = g;
                        neighbour.hCost = h; 
                        neighbour.parent = currentSpot;

                        if (!openSet.Contains(neighbour))
                            openSet.Enqueue(neighbour);
                        else
                        {
                            openSet.UpdateHeap(neighbour);
                        }
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startSpot, targetSpot);
        }
        Debug.Log(pathSuccess); 
        GameManager.PathManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    // 휴리스틱 (Heuristic) : 최상의 경로를 추정하는 순위값, 휴리스틱에 의해 경로탐색 효율이 결정됨
    // Implementing Euclidean 
    private int Heuristic(Cell start, Cell end)
    {
        int xSize = Math.Abs(start.gridX - end.gridX);  // 가로로 가야하는 횟수
        int ySize = Math.Abs(start.gridY - end.gridY);  // 세로로 가야하는 횟수

        // 타일맵용 유클리드 거리 적용 
        int straightCount = Math.Abs(xSize - ySize);
        int diagonalCount = Math.Max(xSize, ySize) - straightCount;
        return CostStraight * straightCount + CostDiagonal * diagonalCount;
    }

    Vector3[] RetracePath(Cell startNode, Cell endNode)
    {
        List<Cell> path = new List<Cell>();
        Cell currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }

    Vector3[] SimplifyPath(List<Cell> path)
    {
        //TODO: Reuse the given path to calculate the distance between the points, and thus calculate whether hear 'could' listen to that given sound. 
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPos);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }
}


