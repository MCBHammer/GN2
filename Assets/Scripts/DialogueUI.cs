using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private Sprite characterSprite;
    
    public bool isOpen { get; private set; }

    private TypewriterEffect typeWriterEffect;

    private void Awake()
    {
        typeWriterEffect = GetComponent<TypewriterEffect>();
    }

    private void Start()
    {
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueList dialogueList)
    {
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueList));
    }

    
    private IEnumerator StepThroughDialogue(DialogueList dialogueList)
    {
        /*
        foreach(string dialogue in dialogueList.Dialogue)
        {
            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftControl));
        }

        CloseDialogueBox();
        */
        yield return null;
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);

        while (typeWriterEffect.isRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                typeWriterEffect.Stop();
            }
        }
    }

    private void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
