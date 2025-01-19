using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawner : MonoBehaviour
{
    public Tilemap groundTilemap;
    public GameObject collectablePrefab;
    public int numberOfItems = 10;
    public float spawnOffsetY = 0.5f;

    private List<Vector3> validPositions = new List<Vector3>();

    void Start()
    {
        GenerateValidPositions();
        SpawnItems();
    }

    void GenerateValidPositions()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int localPlace = (new Vector3Int(x, y, (int)groundTilemap.transform.position.y));
                Vector3 place = groundTilemap.CellToWorld(localPlace);

                if (!groundTilemap.HasTile(localPlace))
                {
                    validPositions.Add(place);
                }
            }
        }
    }

    void SpawnItems()
    {
        if (collectablePrefab == null) return;

        for (int i = 0; i < numberOfItems; i++)
        {
            if (validPositions.Count == 0) break;

            int randomIndex = Random.Range(0, validPositions.Count);
            Vector3 spawnPosition = validPositions[randomIndex];

            Instantiate(collectablePrefab, spawnPosition, Quaternion.identity);

            validPositions.RemoveAt(randomIndex);
        }
    }
}
