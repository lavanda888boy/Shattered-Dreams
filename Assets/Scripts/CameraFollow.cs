using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found");
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 clampedPosition = new(clampedX, clampedY, desiredPosition.z);

        transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
    }
}

