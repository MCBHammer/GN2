using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueList")]
public class DialogueList : ScriptableObject
{
    [SerializeField] public List<DialogueObject> Dialogue;
    public string nextPanelName; //idk just carried this over
}
