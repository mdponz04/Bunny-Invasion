using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace mapNamespace
{
    public class Pathfinding
    {
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private Grid grid;
        private int width;
        private int height;
        //open list for available node to move
        private List<Node> openList;
        //close list is the node list that move over
        private List<Node> closeList;
        public Pathfinding(int width, int height, Grid grid)
        {
            this.grid = grid;
            this.width = width;
            this.height = height;
        }
        public List<Vector3> findpath(Vector3 startWorldPosition, Vector3 endWorldPosition, float cellSize)
        {
            grid.GetXY(startWorldPosition, out int startX, out int startY);
            grid.GetXY(endWorldPosition, out int endX, out int endY);
            List<Node> path = findPath(startX, startY, endX, endY);

            if(path == null)
            {
                return null;
            }

            List<Vector3> vectorPath = new();
            Vector3 vectorOneIn2D = new Vector3(1f, 1f, 0f);
            foreach(Node node in path)
            {
                vectorPath.Add(grid.GetWorldPosition(node.x, node.y) + vectorOneIn2D * cellSize * .5f);
            }

            return vectorPath;
        }
        public List<Node> findPath(int startX, int startY, int endX, int endY)
        {
            //Grid position for start and end node
            Node startNode = grid.GetValue(startX, startY);
            Node endNode = grid.GetValue(endX, endY);
            //Set value for open list
            openList = new List<Node> { startNode };
            closeList = new List<Node>();
            //Set start value for all nodes in grid
            for(int x = 0; x < grid.width; x++)
            {
                for(int y = 0; y < grid.height; y++)
                {
                    Node pathNode = grid.GetValue(x, y);
                    pathNode.gCost = int.MaxValue;
                    pathNode.calculateFCost();
                    pathNode.cameFromNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.calculateFCost();
            //The cycle of finding path
            while(openList.Count > 0)
            {
                Node currentNode = GetLowestFCostNode(openList);
                if(currentNode == endNode)
                {
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closeList.Add(currentNode);

                foreach(Node nearNode in GetNearNodeList(currentNode))
                {
                    if (closeList.Contains(nearNode)) continue;
                    if (!nearNode.isWalkable)
                    {
                        closeList.Add(nearNode);
                        continue;
                    }

                    int temporaryGCost = currentNode.gCost + CalculateDistanceCost(currentNode, nearNode);
                    if(temporaryGCost < nearNode.gCost)
                    {
                        nearNode.cameFromNode = currentNode;
                        nearNode.gCost = temporaryGCost;
                        nearNode.hCost = CalculateDistanceCost(nearNode, endNode);
                        nearNode.calculateFCost();
                    }

                    if (!openList.Contains(nearNode))
                    {
                        openList.Add(nearNode);
                    }
                }
            }
            //Run out of nodes in open list
            return null;
        }
        // Get at most 8 nodes around the current node
        private List<Node> GetNearNodeList(Node currentNode)
        {
            List<Node> nearNodeList = new List<Node>();
            if(currentNode.x -1 >= 0)
            {
                nearNodeList.Add(grid.GetValue(currentNode.x -1, currentNode.y));
                if(currentNode.y -1 >= 0)
                {
                    nearNodeList.Add(grid.GetValue(currentNode.x - 1, currentNode.y -1));
                }
                if(currentNode.y +1 < grid.height)
                {
                    nearNodeList.Add(grid.GetValue(currentNode.x - 1, currentNode.y + 1));
                }
            }
            if(currentNode.x +1 < grid.width)
            {
                nearNodeList.Add(grid.GetValue(currentNode.x + 1, currentNode.y));
                if (currentNode.y - 1 >= 0)
                {
                    nearNodeList.Add(grid.GetValue(currentNode.x + 1, currentNode.y - 1));
                }
                if (currentNode.y + 1 < grid.height)
                {
                    nearNodeList.Add(grid.GetValue(currentNode.x + 1, currentNode.y + 1));
                }
            }

            if(currentNode.y - 1 >= 0)
            {
                nearNodeList.Add(grid.GetValue(currentNode.x, currentNode.y - 1));
            }
            if(currentNode.y + 1 <= grid.height)
            {
                nearNodeList.Add(grid.GetValue(currentNode.x, currentNode.y + 1));
            }

            return nearNodeList;
        }
        //Calculate the whole path then return to List<Node>
        private List<Node> CalculatePath(Node endNode)
        {
            List<Node> path = new List<Node>();
            path.Add(endNode);
            Node currentNode = endNode;
            while(currentNode.cameFromNode != null)
            {
                path.Add(currentNode.cameFromNode);
                currentNode = currentNode.cameFromNode; 
            }
            path.Reverse();
            return path;
        }
        //Calculate cost from node A to node B 
        private int CalculateDistanceCost(Node a, Node b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }
        //Get lowest F cost node in the List
        private Node GetLowestFCostNode(List<Node> nodeList)
        {
            Node lowestFCost = nodeList[0];
            for(int i = 1; i < nodeList.Count; i++)
            {
                if(nodeList[i].fCost < lowestFCost.fCost)
                {
                    lowestFCost = nodeList[i];
                }
            }
            return lowestFCost;
        }
        //Check collider inside the cell
        public bool IsCellOccupied(int x, int y, float cellSize)
        {
            Vector3 vectorOne2D = new Vector3(1f, 1f, 0f);
            // Get the corner position of the cell
            Vector3 cellCornerPosition = grid.GetWorldPosition(x, y);

            // Calculate the center of the cell (corner + half cell size)
            Vector2 cellCenterPosition = cellCornerPosition + vectorOne2D;

            // Use OverlapBox for accurate 2D collision checking inside the square cell
            Collider2D collider = Physics2D.OverlapBox(cellCenterPosition, new Vector2(cellSize, cellSize), 0f);
            if(collider != null)
            {
                Debug.Log("is occupied: " + collider != null);
            }
            // Return true if a collider is found
            return collider != null;
        }
        public void UpdateWalkableNodes()
        {

            for(int x = 0; x < grid.width; x++)
            {
                for(int y = 0; y < grid.height; y++)
                {
                    IsCellOccupied(x, y, grid.cellSize);
                }
            }
        }
    }
}

