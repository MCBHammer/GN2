using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingZone : MonoBehaviour
{
    [SerializeField] private int nextLevelIndex;
    [SerializeField] private int spawnPointIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevelIndex);
            SpawnPointTracker.Instance.spawnPoint = spawnPointIndex;
        }
    }
}
