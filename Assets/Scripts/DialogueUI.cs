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
    [SerializeField] private Slider timeSlider;
    
    public bool isOpen { get; private set; }

    private TypewriterEffect typeWriterEffect;

    private DialogueList dialogueList;
    private Queue<DialogueObject> dialogueQueue = new Queue<DialogueObject>();

    private float timeElapsed = 0;
    private float timeMaximum = 5f;
    private bool dialogueAdvance = false;
    private bool readyToAdvance = false;

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
            timeElapsed = 0;
            dialogueAdvance = false;

            DialogueObject item = dialogueQueue.Dequeue();
            nameLabel.text = item.SpeakerName;
            if(characterSprite != null)
            {
                characterSprite.sprite = item.CharacterSprite;
            }
            yield return RunTypingEffect(item.Dialogue);
            textLabel.text = item.Dialogue;

            yield return null;
            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftControl));
            timeMaximum = item.DialogueTime;
            readyToAdvance = true;
            while (!dialogueAdvance)
            {
                yield return null;
            }
            timeElapsed = 0;
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

    private void Update()
    {
        if (readyToAdvance)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                dialogueAdvance = true;
                readyToAdvance = false;
            }

            timeElapsed += Time.deltaTime;
            if (timeElapsed > timeMaximum)
            {
                dialogueAdvance = true;
                readyToAdvance = false;
            }
        }

        timeSlider.value = timeElapsed / timeMaximum;
    }

    private void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
