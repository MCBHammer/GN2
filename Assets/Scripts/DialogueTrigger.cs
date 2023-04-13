using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueList dialogueList;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerData player))
        {
            player.DialogueUI.TriggeredDialogue(dialogueList);
            player.DialogueUI.ShowDialogue();
            this.gameObject.SetActive(false);
        }
    }
}
