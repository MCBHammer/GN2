using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerData player))
        {
            player.DialogueUI.ShowDialogue(dialogueObject);
            this.gameObject.SetActive(false);
        }
    }
}
