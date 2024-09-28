using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mapNamespace;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;
using UnityEngine.Rendering.Universal;
using System.ComponentModel.Design.Serialization;


public class Testing : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    private mapNamespace.Grid grid;
    private Pathfinding pathfinding;
    Vector3 gridOriginPosition;
    int width, height;
    private void Start()
    {
        width = tilemap.cellBounds.xMax + 1;
        height = tilemap.cellBounds.yMax + 1;
        gridOriginPosition = tilemap.cellBounds.min;

        Debug.Log("tile map from: " + tilemap.cellBounds.min + " to: " + tilemap.cellBounds.max);
        grid = new mapNamespace.Grid(width, height, tilemap.cellSize.x, gridOriginPosition);
        pathfinding = new Pathfinding(width, height, grid);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            float cellSize = tilemap.cellSize.x;
            //Debug.Log("Cell size: " + cellSize);
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            grid.GetXY(mouseWorldPosition, out int x, out int y);
            
            List<Node> path = pathfinding.findPath(0, 0, x, y);
            if(path != null)
            {
                for(int i = 0; i < path.Count; i++)
                {
                    
                    if(i + 1 < path.Count)
                    {
                        Debug.DrawLine(new Vector3(path[i].x, path[i].y) * cellSize + Vector3.one * cellSize * 0.5f, new Vector3(path[i + 1].x, path[i + 1].y) * cellSize + Vector3.one * cellSize * 0.5f, Color.green, 10f);
                    }
                    
                }
            }
        }
    }



}
