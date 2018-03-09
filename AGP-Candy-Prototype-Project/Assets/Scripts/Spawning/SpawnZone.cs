using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour {

    private BoxCollider boxSpawnZone;

    [SerializeField]
    private int spawnPriority = 1;

    [SerializeField]
    private GameObject[] SpawnablePrefabs;

    private void Start()
    {
        boxSpawnZone = GetComponent<BoxCollider>();
        boxSpawnZone.isTrigger = true;
    }

    // Spawns a random prefab from the list in a random spot in the box
    public GameObject SpawnObject()
    {
        if (SpawnablePrefabs.Length > 0)
        {
            int indexToSpawn = Random.Range(0, SpawnablePrefabs.Length);
            GameObject spawnedObject = Instantiate(SpawnablePrefabs[indexToSpawn], GetSpawnPosition(), GetSpawnRotation());
            return spawnedObject;
        }
        else
        {
            Debug.LogError("Tried to spawn an object on a SpawnZone with no SpawnablePrefabs attached.");
            return null;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        return transform.position + RandomPointInBox(boxSpawnZone.center, boxSpawnZone.size);
    }

    private Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3(
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }

    private Quaternion GetSpawnRotation()
    {
        Debug.LogWarning("TODO: figure out what designer wants this to be");
        return transform.rotation;
    }

    public int GetSpawnPriority() { return spawnPriority; }

}
