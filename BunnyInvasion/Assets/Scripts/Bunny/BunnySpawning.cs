using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace BunnyNamespace
{
    public class BunnySpawning : MonoBehaviour
    {
        //Bunny prefabs for spawning
        [SerializeField] private GameObject bunnyPrefab;
        //Grid for back ground
        [SerializeField] private Tilemap background;
        [SerializeField] private GameHandler gameHandler;

        private Vector3 maxWorldPosition;
        private Vector3 minWorldPosition;
        /*private bool canSpawn;*/
        private float elapsedTime;
        private float spawnCooldown = 10f;
        private float lastTimeSpawn = 0f;

        // Start is called before the first frame update
        private void Start()
        {
            // Get the bounds of the tilemap in grid cells
            BoundsInt tilemapBounds = background.cellBounds;

            // Get min and max positions of the grid cells
            Vector3Int minCellPosition = tilemapBounds.min;
            Vector3Int maxCellPosition = tilemapBounds.max;

            // Convert grid cell positions to world positions using tilemap
            minWorldPosition = background.GetCellCenterWorld(minCellPosition);
            maxWorldPosition = background.GetCellCenterWorld(maxCellPosition);

            /*canSpawn = true;*/

            elapsedTime = gameHandler.GetElapsedTime();
        }

        // Update is called once per frame
        private void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.B) && canSpawn == true)
            {
                Spawn();
                canSpawn = false;
            }

            if (Input.GetKeyUp(KeyCode.B))
            {
                canSpawn = true;
            }*/



            //Continuosly update elapsed time 
            elapsedTime = gameHandler.GetElapsedTime();
            TimingSpawnCooldown();
        }
        private void TimingSpawnCooldown()
        {
            float nextTimeSpawn = lastTimeSpawn + spawnCooldown;
            if (elapsedTime >= nextTimeSpawn)
            {
                Spawn();
                lastTimeSpawn = elapsedTime;
            }
        }
        private void Spawn()
        {
            SpawnBunnyOnGrid(GetRandomWorldPosition2D(minWorldPosition, maxWorldPosition));
        }
        private void SpawnBunnyOnGrid(Vector3 worldPosition)
        {
            // Convert world position to grid cell position
            Vector3Int cellPosition = background.WorldToCell(worldPosition);
            // Convert cell position back to world position (to align with grid)
            Vector3 alignedPosition = background.GetCellCenterWorld(cellPosition);
            // Spawn the bunny at the aligned grid position
            // Quaternion.identity is no rotation of spawn object
            Instantiate(bunnyPrefab, alignedPosition, Quaternion.identity);

            Debug.Log("Spawn bunny in:" + worldPosition);
        }

        private Vector3 GetRandomWorldPosition2D(Vector3 minWorld, Vector3 maxWorld)
        {
            float randomX = Random.Range(minWorld.x, maxWorld.x);
            float randomY = Random.Range(minWorld.y, maxWorld.y);
            Vector3 worldPosition = new Vector3(randomX, randomY, 0f);
            return worldPosition;
        }
    }
}

