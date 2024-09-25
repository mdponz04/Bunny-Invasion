using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mapNamespace
{
    public class Node
    {
        //Vector2Int for the grid-based position
        public Vector2Int gridPosition {  get; set; }
        private float gCost {  get; set; }
        public float hCost {  get; set; }
        public Node parentNode { get; set; }
        public bool isWalkable { get; set; }

        public Node(Vector2Int gridPosition, bool isWalkable)
        {
            this.gridPosition = gridPosition;
            this.isWalkable = isWalkable;
        }

        //Get fCost
        public float fCost() => gCost + hCost;
    }
}

