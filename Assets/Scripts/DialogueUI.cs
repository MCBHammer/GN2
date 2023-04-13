using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private Image characterSprite;
    
    public bool isOpen { get; private set; }

    private TypewriterEffect typeWriterEffect;

    private DialogueList dialogueList;
    private Queue<DialogueObject> dialogueQueue = new Queue<DialogueObject>();

    private void Awake()
    {
        typeWriterEffect = GetComponent<TypewriterEffect>();
    }

    private void Start()
    {
        CloseDialogueBox();
    }

    public void ShowDialogue()
    {
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueList));
    }

    public void TriggeredDialogue(DialogueList dialogue)
    {
        dialogueList = dialogue;
    }

    
    private IEnumerator StepThroughDialogue(DialogueList dialogueList)
    {
        foreach (DialogueObject item in dialogueList.Dialogue)
        {
            dialogueQueue.Enqueue(item);
        }

        while(dialogueQueue.Count > 0)
        {
            DialogueObject item = dialogueQueue.Dequeue();
            nameLabel.text = item.SpeakerName;
            if(characterSprite != null)
            {
                characterSprite.sprite = item.CharacterSprite;
            }
            yield return RunTypingEffect(item.Dialogue);
            textLabel.text = item.Dialogue;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftControl));
        } 
        
        CloseDialogueBox();

        /*
            foreach (string dialogue in dialogueList.Dialogue)
            {
                yield return RunTypingEffect(dialogue);

                textLabel.text = dialogue;
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftControl));
            }
        */
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
