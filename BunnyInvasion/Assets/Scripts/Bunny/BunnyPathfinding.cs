using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mapNamespace;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Runtime.CompilerServices;
using UnityEngine.Tilemaps;
using PlayerNamespace;

namespace BunnyNamespace
{
    public class BunnyPathfinding : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Player player;

        private mapNamespace.Grid grid;
        private Pathfinding pathfinding;
        private Vector3 gridOriginPosition;
        private int width, height;
        private List<Vector3> pathVectorList;
        private float cellSize;

        private void Start()
        {
            width = tilemap.cellBounds.xMax + 1;
            height = tilemap.cellBounds.yMax + 1;
            gridOriginPosition = tilemap.cellBounds.min;

            Debug.Log("tile map from: " + tilemap.cellBounds.min + " to: " + tilemap.cellBounds.max);
            grid = new mapNamespace.Grid(width, height, tilemap.cellSize.x, gridOriginPosition);
            pathfinding = new Pathfinding(width, height, grid);
            cellSize = grid.cellSize;
        }
        public void SetTargetPosition(Vector3 currentPosition, out int currentPathIndex, out List<Vector3> pathVectorList)
        {
            Vector3 targetPosition = player.GetTransformPosition();
            currentPathIndex = 0;
            pathVectorList = pathfinding.findpath(currentPosition, targetPosition, cellSize);

            if (pathVectorList != null && pathVectorList.Count > 1)
            {
                pathVectorList.RemoveAt(0);
            }

            Debug.Log("target position: " + targetPosition);
        }
        public void UpdateWalkableNodes()
        {
            pathfinding.UpdateWalkableNodes();
        }
    }
}

