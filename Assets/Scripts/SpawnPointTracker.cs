using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointTracker : MonoBehaviour
{
    public int spawnPoint;

    public static SpawnPointTracker Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }  
    }
}
