using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    private Transform playerPosition;
    [SerializeField] private Transform[] spawnPoints;
    private SpawnPointTracker spawnPointTracker;

    private void Start()
    {
        spawnPointTracker = SpawnPointTracker.Instance;
        playerPosition = this.gameObject.transform;
        if(spawnPoints != null)
        {
            playerPosition.position = spawnPoints[spawnPointTracker.spawnPoint].position;
        }
    }
}
