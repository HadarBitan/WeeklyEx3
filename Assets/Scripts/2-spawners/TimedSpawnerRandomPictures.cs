using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/**
 * This component instantiates a given prefab at random time intervals and random bias from its object position.
 */
public class TimedSpawnerRandomPictures: MonoBehaviour {
    // [SerializeField] Mover prefabToSpawn1;
    // [SerializeField] Mover prefabToSpawn2;
    // [SerializeField] Mover prefabToSpawn3;
    // [SerializeField] Mover prefabToSpawn4;

    [SerializeField] private Mover[] prefabs;

    [SerializeField] Vector3 velocityOfSpawnedObject;
    [Tooltip("Minimum time between consecutive spawns, in seconds")] [SerializeField] float minTimeBetweenSpawns = 0.2f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")] [SerializeField] float maxTimeBetweenSpawns = 1.0f;
    [Tooltip("Maximum distance in X between spawner and spawned objects, in meters")] [SerializeField] float maxXDistance = 1.5f;

    void Start() {
        this.StartCoroutine(SpawnRoutine());    // co-routines
    }

    IEnumerator SpawnRoutine() {    // co-routines
        while (true) {
            float timeBetweenSpawnsInSeconds = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(timeBetweenSpawnsInSeconds);       // co-routines
            Vector3 positionOfSpawnedObject = new Vector3(
                transform.position.x + Random.Range(-maxXDistance, +maxXDistance),
                transform.position.y,
                transform.position.z);
            
            //select a random prefeb to spawn
            int randomIndex = Random.Range(0, prefabs.Length);
            Mover prefabToSpawn = prefabs[randomIndex];

            GameObject newObject = Instantiate(prefabToSpawn.gameObject, positionOfSpawnedObject, Quaternion.identity);
            newObject.GetComponent<Mover>().SetVelocity(velocityOfSpawnedObject);
        }
    }

    // public enum PrefabType
    // {
    //     PrefabToSpawn1,
    //     PrefabToSpawn2,
    //     PrefabToSpawn3,
    //     PrefabToSpawn4
    // }
}
