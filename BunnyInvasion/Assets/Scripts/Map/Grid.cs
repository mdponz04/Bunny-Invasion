using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mapNamespace
{
    public class Grid
    {
        public int width {  get; private set; }
        public int height {  get; private set; }
        public float cellSize { get; private set; }
        private Node[,] gridArray;
        private Vector3 originPosition;
        private object value;

        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            gridArray = new Node[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    SetValue(x, y, new Node(this, x, y));
                    //Draw line for debuging
                    UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 10, Color.red, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.blue, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.blue, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.blue, 100f);
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.blue, 100f);
        }

        public Grid(int width, int height, Node value)
        {
            this.width = width;
            this.height = height;
            this.value = value;
        }

        //Convert grid cell position to world position
        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + originPosition;
        }
        //Convert world position to grid cell position
        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        }
        //Set node value for cell position
        public void SetValue(int x, int y, Node value)
        {
            if(x >= 0 && x < width && y >= 0 && y < height)
            {
                gridArray[x, y] = value;
            }
        }
        //Set node value for cell's world position 
        public void SetValue(Vector3 worldPosition, Node value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetValue(x, y, value);
        }
        //Get value for cell position
        public Node GetValue(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return null;
            }
        }
        //Get value for cell's world position 
        public Node GetValue(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);

            return GetValue(x, y);
        }
    }
}