using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject collectablePrefab;
    public int numberOfItems = 15;
    public float minDistance = 5.0f;

    private List<Vector3> groundPositions = new List<Vector3>();
    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        FindGroundPositions();
        SpawnItems();
    }

    void FindGroundPositions()
    {
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground");

        foreach (GameObject ground in groundObjects)
        {
            groundPositions.Add(ground.transform.position);
        }
    }

    void SpawnItems()
    {
        if (collectablePrefab == null) return;

        for (int i = 0; i < numberOfItems; i++)
        {
            if (groundPositions.Count == 0) break;

            Vector3 spawnPosition;
            int attempts = 0;

            do
            {
                if (attempts > 100)
                {
                    Debug.LogWarning("Unable to find a valid position for item placement.");
                    return;
                }

                int randomIndex = Random.Range(0, groundPositions.Count);
                spawnPosition = groundPositions[randomIndex] + new Vector3(0, 3.0f, 0);
                attempts++;
            }
            while (!IsFarEnoughFromOthers(spawnPosition));

            Instantiate(collectablePrefab, spawnPosition, Quaternion.identity);

            usedPositions.Add(spawnPosition);

            groundPositions.Remove(spawnPosition);
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

    bool IsCollidingWithObjects(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 2.0f);
        return colliders.Length > 0;
    }
}
