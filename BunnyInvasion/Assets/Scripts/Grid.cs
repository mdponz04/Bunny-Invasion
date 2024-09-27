using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mapNamespace
{
    public class Grid
    {
        private int width;
        private int height;
        private float cellSize;
        private int[,] gridArray;

        public Grid(int width, int height, float cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            gridArray = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 30, Color.red, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.blue, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.blue, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.blue, 100f);
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.blue, 100f);
        }

        //Convert grid cell position to world position
        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize;
        }
        //Convert world position to grid cell position
        public void GetXYCell(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt(worldPosition.x / cellSize);
            y = Mathf.FloorToInt(worldPosition.y / cellSize);
        }
        //Set value for cell position
        public void SetValue(int x, int y, int value)
        {
            if(x >= 0 && x < width && y >= 0 && y < height)
            {
                gridArray[x, y] = value;
            }
        }
        //Set value for cell's world position 
        public void SetValue(Vector3 worldPosition, int value)
        {
            int x, y;
            GetXYCell(worldPosition, out x, out y);
            SetValue(x, y, value);
        }
    }
}