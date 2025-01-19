using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawner : MonoBehaviour
{
    public Tilemap groundTilemap;
    public GameObject collectablePrefab;
    public int numberOfItems = 10;
    public float minDistance = 2.0f;

    private List<Vector3> validPositions = new List<Vector3>();
    private List<Vector3> usedPositions = new List<Vector3>();

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
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                if (groundTilemap.HasTile(tilePosition))
                {
                    Vector3Int tileAbove = new Vector3Int(x, y + 1, 0);
                    if (!groundTilemap.HasTile(tileAbove))
                    {
                        Vector3 worldPosition = groundTilemap.CellToWorld(tilePosition) +
                                                new Vector3(0, groundTilemap.cellSize.y + 3f, 0);
                        validPositions.Add(worldPosition);
                    }
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

            Vector3 spawnPosition;
            int attempts = 0;

            do
            {
                if (attempts > 100)
                {
                    Debug.LogWarning("Unable to find a valid position for item placement.");
                    return;
                }

                int randomIndex = Random.Range(0, validPositions.Count);
                spawnPosition = validPositions[randomIndex];
                attempts++;
            }
            while (!IsFarEnoughFromOthers(spawnPosition));

            Instantiate(collectablePrefab, spawnPosition, Quaternion.identity);

            usedPositions.Add(spawnPosition);

            validPositions.Remove(spawnPosition);
        }
    }

    bool IsFarEnoughFromOthers(Vector3 position)
    {
        foreach (Vector3 usedPosition in usedPositions)
        {
            if (Vector3.Distance(position, usedPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}
