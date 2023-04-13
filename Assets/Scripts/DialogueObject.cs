using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueObject
{
    [SerializeField] private string speakerName;
    [SerializeField] [TextArea] private string dialogue;
    [SerializeField] private Sprite characterSprite;

    public string SpeakerName => speakerName;
    public string Dialogue => dialogue;
    public Sprite CharacterSprite => characterSprite;
}
