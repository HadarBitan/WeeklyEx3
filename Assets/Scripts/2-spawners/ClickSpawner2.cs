using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component spawns the given object whenever the player clicks a given key.
 */
public class ClickSpawner2: MonoBehaviour {
    [SerializeField] protected InputAction spawnAction = new InputAction(type: InputActionType.Button);
    [SerializeField] protected GameObject prefabToSpawn;
    [SerializeField] protected Vector3 velocityOfSpawnedObject;
    [SerializeField] float secondsBetweenSpawns = 1f;

    private bool canShoot = true;

    void OnEnable()  {
        spawnAction.Enable();
    }

    void OnDisable()  {
        spawnAction.Disable();
    }

    void Update()
    {
        if (canShoot && spawnAction.ReadValue<float>() > 0)
        {
            StartCoroutine(ShootPause());
        }
    }

    protected virtual GameObject Shoot() {
        Debug.Log("Spawning a new object");

        // Step 1: spawn the new object.
        Vector3 positionOfSpawnedObject = transform.position;  // span at the containing object position.
        Quaternion rotationOfSpawnedObject = Quaternion.identity;  // no rotation.
        GameObject newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, rotationOfSpawnedObject);

        // Step 2: modify the velocity of the new object.
        Mover newObjectMover = newObject.GetComponent<Mover>();
        if (newObjectMover) {
            newObjectMover.SetVelocity(velocityOfSpawnedObject);
        }

        return newObject;

    }


    IEnumerator ShootPause() {
        canShoot = false; // Disable shooting temporarily
        Shoot();
        yield return new WaitForSeconds(secondsBetweenSpawns);
        canShoot = true; // Enable shooting again
    }
}
