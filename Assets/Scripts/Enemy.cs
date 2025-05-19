using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Terrain terrain;
    public float edgeMargin = 10f;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
