using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Terrain terrain;
    public float edgeMargin = 10f;
    public float moveSpeed = 1f;
    public float turnSpeed = 120f;
    public float obstacleDetectionDistance = 2f;

    //private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (terrain == null)
        {
            terrain = Terrain.activeTerrain;
        }

        // Get terrain origin and size
        Vector3 terrainPos = terrain.GetPosition();
        Vector3 terrainSize = terrain.terrainData.size;

        // Calculate valid spawn ranges with edge margin
        float minX = terrainPos.x + edgeMargin;
        float maxX = terrainPos.x + terrainSize.x - edgeMargin;
        float minZ = terrainPos.z + edgeMargin;
        float maxZ = terrainPos.z + terrainSize.z - edgeMargin;

        // Pick random position within safe area
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        // Sample terrain height at location
        float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPos.y;

        // Get collider-based vertical offset (fallback to 1f if missing)
        Collider col = GetComponent<Collider>();
        float heightOffset = col != null ? col.bounds.extents.y : 1f;

        // Set final spawn position above ground
        transform.position = new Vector3(x, y + heightOffset, z);

        // Set initial random move direction
        //moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        // Check for obstacle
        if (Physics.Raycast(transform.position, transform.forward, obstacleDetectionDistance))
        {
            // Pick a new random direction when an obstacle is detected
            float randomAngle = Random.Range(90f, 180f);
            transform.Rotate(0, randomAngle, 0);
        }

        // Move forward
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Stay on terrain
        Vector3 pos = transform.position;
        float y = terrain.SampleHeight(pos) + terrain.GetPosition().y;

        // Adjust Y to terrain and keep same XZ
        Collider col = GetComponent<Collider>();
        float heightOffset = col != null ? col.bounds.extents.y : 1f;
        transform.position = new Vector3(pos.x, y + heightOffset, pos.z);
    }
}
