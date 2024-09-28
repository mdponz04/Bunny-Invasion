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
    }
}

