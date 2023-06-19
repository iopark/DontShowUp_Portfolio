using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AStar
{
    const int CostStraight = 10;
    const int CostDiagonal = 14;

   // static Point[] Direction =
   // {
   //         new Point(  0, +1 ),			// ��
			//new Point(  0, -1 ),			// ��
			//new Point( -1,  0 ),			// ��
			//new Point( +1,  0 ),			// ��
			//new Point( -1, +1 ),		    // �»�
			//new Point( -1, -1 ),		    // ����
			//new Point( +1, +1 ),		    // ���
			//new Point( +1, -1 )		        // ����
   // };

    public Vector3[] FindPath() { }
    public static bool PathFinding(in bool[,] tileMap, in Point start, in Point end, in bool cross, out List<Point> path)
    {
        int ySize = tileMap.GetLength(0);
        int xSize = tileMap.GetLength(1);

        ASNode[,] nodes = new ASNode[ySize, xSize];
        bool[,] visited = new bool[ySize, xSize];
        PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();

        // 0. ���� ������ �����Ͽ� �߰�
        ASNode startNode = new ASNode(start, null, 0, Heuristic(start, end));
        nodes[startNode.point.y, startNode.point.x] = startNode;
        nextPointPQ.Enqueue(startNode, startNode.f);

        while (nextPointPQ.Count > 0)
        {
            // 1. �������� Ž���� ���� ������
            ASNode nextNode = nextPointPQ.Dequeue();

            // 2. �湮�� ������ �湮ǥ��
            visited[nextNode.point.y, nextNode.point.x] = true;

            // 3. �������� Ž���� ������ �������� ���
            // �����ߴٰ� �Ǵ��ؼ� ��� ��ȯ
            if (nextNode.point.x == end.x && nextNode.point.y == end.y)
            {
                Point? pathPoint = end;
                path = new List<Point>();

                while (pathPoint != null)
                {
                    Point point = pathPoint.GetValueOrDefault();
                    path.Add(point);
                    pathPoint = nodes[point.y, point.x].parent;
                }

                path.Reverse();
                return true;
            }

            // 4. AStar Ž���� ����
            // ���� Ž��
            for (int i = 0; i < Direction.Length; i++)
            {
                int x = nextNode.point.x + Direction[i].x;
                int y = nextNode.point.y + Direction[i].y;

                // 4-1. Ž���ϸ� �ȵǴ� ���
                // ���� ����� ���
                if (x < 0 || x >= xSize || y < 0 || y >= ySize)
                    continue;
                // Ž���� �� ���� ������ ���
                else if (tileMap[y, x] == false)
                    continue;
                // �̹� �湮�� ������ ���
                else if (visited[y, x])
                    continue;
                // �밢������ �̵��� �Ұ��� ������ ���
                else if (i >= 4)
                {
                    if (!tileMap[y, nextNode.point.x] && !tileMap[nextNode.point.y, x])
                        continue;
                    if (cross && tileMap[y, nextNode.point.x] ^ tileMap[nextNode.point.y, x])
                        continue;
                }

                // 4-2. Ž���� ���� �����
                int g = nextNode.g + ((nextNode.point.x == x || nextNode.point.y == y) ? CostStraight : CostDiagonal);
                int h = Heuristic(new Point(x, y), end);
                ASNode newNode = new ASNode(new Point(x, y), nextNode.point, g, h);

                // 4-3. ������ ������ �ʿ��� ��� ���ο� �������� �Ҵ�
                if (nodes[y, x] == null ||      // Ž������ ���� �����̰ų�
                    nodes[y, x].f > newNode.f)  // ����ġ�� ���� ������ ���
                {
                    nodes[y, x] = newNode;
                    nextPointPQ.Enqueue(newNode, newNode.f);
                }
            }
        }

        path = null;
        return false;
    }

    // �޸���ƽ (Heuristic) : �ֻ��� ��θ� �����ϴ� ������, �޸���ƽ�� ���� ���Ž�� ȿ���� ������
    private static int Heuristic(Point start, Point end)
    {
        int xSize = Math.Abs(start.x - end.x);  // ���η� �����ϴ� Ƚ��
        int ySize = Math.Abs(start.y - end.y);  // ���η� �����ϴ� Ƚ��

        // ����ư �Ÿ� : ������ ���� �̵��ϴ� �Ÿ�
        // return CostStraight * (xSize + ySize);

        // ��Ŭ���� �Ÿ� : �밢���� ���� �̵��ϴ� �Ÿ�
        // return CostStraight * (int)Math.Sqrt(xSize * xSize + ySize * ySize);

        // Ÿ�ϸʿ� ��Ŭ���� �Ÿ� : ������ �밢���� ���� �̵��ϴ� �Ÿ�
        int straightCount = Math.Abs(xSize - ySize);
        int diagonalCount = Math.Max(xSize, ySize) - straightCount;
        return CostStraight * straightCount + CostDiagonal * diagonalCount;
    }

    private class ASNode
    {
        public Point point;     // ���� ����
        public Point? parent;   // �� ������ Ž���� ����

        public int g;           // ��������� ��, �� ���ݱ��� ��� ����ġ
        public int h;           // ������ ����Ǵ� ��, ��ǥ���� ���� ��� ����ġ
        public int f;           // f(x) = g(x) + h(x);

        public ASNode(Point point, Point? parent, int g, int h)
        {
            this.point = point;
            this.parent = parent;
            this.g = g;
            this.h = h;
            this.f = g + h;
        }
    }

}


