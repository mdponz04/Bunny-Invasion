using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace mapNamespace
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Grid grid;
        private int width;
        private int height;
        private float cellSize;
        public Node[,] gridArray;

        // Start method to initialize the grid
        void Start()
        {
            InitializeGridSize();
            CreateGrid();
        }
        // Calculate the width and height of the grid based on tilemap bounds
        private void InitializeGridSize()
        {
            if (tilemap != null)
            {
                // Get bounds of the tilemap
                BoundsInt tilemapBounds = tilemap.cellBounds;

                // Use the bounds to determine width and height
                width = tilemapBounds.size.x +1;
                height = tilemapBounds.size.y +1;
                
                
            }
            //Because I suppose to make square cells for tilemap so just got 1 edge is enough
            cellSize = tilemap.cellSize.x;
            Debug.Log($"Grid Width: {width}, Grid Height: {height}");
        }

        // Method to create and initialize the grid
        public void CreateGrid()
        {
            gridArray = new Node[width, height];
            Debug.Log("The map is null: " + gridArray.IsUnityNull());
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector2Int gridPosition = new Vector2Int(x, y);
                    Vector3 worldPosition = GetWorldPositionFromGridPosition(gridPosition);

                    // Cast a ray in circle shape inside the cell or check collision to see if the node is blocked
                    bool isWalkable = !Physics2D.OverlapCircle(worldPosition, cellSize / 2);

                    gridArray[x, y] = new Node(gridPosition, isWalkable);
                }
            }
        }
        // Get a node from a world position
        public Node GetNodeFromWorldPosition(Vector2 worldPosition)
        {
            int x = Mathf.FloorToInt(worldPosition.x / cellSize);
            int y = Mathf.FloorToInt(worldPosition.y / cellSize);

            // Ensure the coordinates are within the grid bounds
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return gridArray[x, y];
            }

            return null; // Return null if out of bounds
        }
        // Transform from grid position to world position
        public Vector3 GetWorldPositionFromGridPosition(Vector2Int gridPosition)
        {
            return new Vector3(gridPosition.x * cellSize, gridPosition.y * cellSize);
        }

        void OnDrawGizmos()
        {
            if (gridArray != null)
            {
                foreach (var node in gridArray)
                {
                    // Determine color based on walkability
                    Gizmos.color = node.isWalkable ? Color.white : Color.red;

                    // Draw a cube or wireframe to represent each grid cell
                    Vector3 worldPos = tilemap.CellToWorld((Vector3Int)node.gridPosition) + new Vector3(cellSize / 2, cellSize / 2, 0);
                    Gizmos.DrawWireCube(worldPos, new Vector3(cellSize, cellSize, 0));
                }
            }
        }
    }
}

