using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mapNamespace;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;


public class Testing : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    private mapNamespace.Grid grid;
    private void Start()
    {
        int width = tilemap.cellBounds.xMax + 1;
        int height = tilemap.cellBounds.yMax + 1;
        Debug.Log("tile map from: " + tilemap.cellBounds.min + " to: " + tilemap.cellBounds.max);
        grid = new mapNamespace.Grid(width, height, tilemap.cellSize.x);
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
        }
    }



}
