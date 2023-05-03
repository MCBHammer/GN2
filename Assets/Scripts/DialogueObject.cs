using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueObject
{
    [SerializeField] private string speakerName;
    [SerializeField] [TextArea] private string dialogue;
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private float dialogueTime = 5f;

    public string SpeakerName => speakerName;
    public string Dialogue => dialogue;
    public Sprite CharacterSprite => characterSprite;
    public float DialogueTime => dialogueTime;
}
