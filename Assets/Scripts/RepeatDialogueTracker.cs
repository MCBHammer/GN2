using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatDialogueTracker : MonoBehaviour
{
    public List<GameObject> dialoguesTriggered;
    public static RepeatDialogueTracker Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach(GameObject dialogue in dialoguesTriggered)
        {
            Destroy(dialogue);
        }
    }
}
