using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    private Vector2 bunnyGridPosition;
    private int height;
    private int width;

    public LevelGrid(int height, int width)
    {
        this.height = height;
        this.width = width;
    }

    private void spawnRandom()
    {
        bunnyGridPosition = new Vector2(Random.Range(0, width), Random.Range(0, height));
    }
}
