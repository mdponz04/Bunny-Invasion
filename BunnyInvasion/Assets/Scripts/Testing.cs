using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mapNamespace;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;
using UnityEngine.Rendering.Universal;
using System.ComponentModel.Design.Serialization;
using UnityEngine.EventSystems;


public class Testing : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    private mapNamespace.Grid grid;
    private Pathfinding pathfinding;
    private Vector3 gridOriginPosition;
    private int width, height;
    private List<Vector3> pathVectorList;
    private int currentPathIndex;
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

    private void Update()
    {





        /*if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Cell size: " + cellSize);
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            *//*grid.GetXY(mouseWorldPosition, out int x, out int y);
            
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
            }*//*
            SetTargetPosition(mouseWorldPosition);
        }

        HandleMove();
        */
    }
    /*private void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = pathfinding.findpath(GetPosition(), targetPosition, cellSize);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
        
        Debug.Log("target position: " + targetPosition);
        Debug.Log("length: " + pathVectorList.Count + " cells");
    }

    private void HandleMove()
    {
        if(pathVectorList != null)
        {
            pathfinding.UpdateWalkableNodes();
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            
            if(Vector3.Distance(transform.position, targetPosition) > .5f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                *//*Debug.Log("Move direction vector: " + moveDir);*//*
                moveDir.z = 0f;
                
                float moveSpeed = 10f;
                transform.position += moveDir * Time.deltaTime * moveSpeed;
            }
            else
            {
                currentPathIndex++;
                if(currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }
    private void StopMoving()
    {
        pathVectorList = null;
    }
    private Vector3 GetPosition()
    {
        return transform.position;
    }*/
}
