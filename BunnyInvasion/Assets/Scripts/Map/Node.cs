using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mapNamespace
{
    public class Node
    {
        private Grid grid;
        public int x {  get; set; }
        public int y { get; set; }

        public int gCost { get; set; }
        public int hCost { get; set; }
        public int fCost { get; set; }
        public Node cameFromNode { get; set; }
        public bool isWalkable { get; set; }
        public Node(Grid grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            isWalkable = true;
        }
        public void calculateFCost()
        {
            fCost = gCost + hCost;
        }
        public override string ToString()
        {
            return x + ", " + y;
        }
        //Check collider inside the node
        private bool IsNodeOccupied()
        {
            Vector3 vectorOne2D = new Vector3(1f, 1f, 0f);
            // Get the corner position of the cell
            Vector3 cellCornerPosition = grid.GetWorldPosition(x, y);

            // Calculate the center of the cell (corner + half cell size)
            Vector2 cellCenterPosition = cellCornerPosition + vectorOne2D;

            // Use OverlapBox for accurate 2D collision checking inside the square cell
            Collider2D collider = Physics2D.OverlapBox(cellCenterPosition, new Vector2(grid.cellSize, grid.cellSize), 0f);
            /*if (collider != null)
            {
                Debug.Log("is occupied by " + collider.name + " at: (" + x + ", " + y + ")");
            }*/
            // Return true if a collider is found
            return collider != null;
        }
        public void UpdateWalkable()
        {
            if (IsNodeOccupied())
            {
                isWalkable = false;
            }
            else
            {
                isWalkable = true;
            }
        }
    }
}

