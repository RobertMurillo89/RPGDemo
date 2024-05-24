using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public GameObject dialogueBox;
    public GameObject nameBox;

    public string[] dialogueLines;

    public int currentLine;
    private bool justStarted;

    string questToMark;
    bool markQuestComplete;
    bool shouldMarkQuest;

    public static DialogueManager instance;

    public GameObject UIInstruction;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

        if (dialogueBox.activeInHierarchy && Input.GetKeyUp(KeyCode.E))
        {
            if (justStarted)
            {
                justStarted = false;
            }
            else
            {
                AdvanceDialogue();
            }
        }

    }

    public void ShowDialogue(string[] newLines, bool isPerson)
    {
        dialogueLines = newLines;

        currentLine = 0;
        justStarted = true;
        CheckIfName();

        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive (true);

        

        nameBox.SetActive(isPerson);

        GameManager.instance.dialogueActive = true;
    }

    public void CheckIfName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }

    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;

        shouldMarkQuest = true;
    }

    public void ActivateUIInstruction()
    {
        UIInstruction.SetActive (true);

    }

    public void DeactivateUIInstructions()
    {
        UIInstruction.SetActive(false);

    }

    private void AdvanceDialogue()
    {
        currentLine++;
        //if (currentLine >= dialogueLines.Length)
        //{
        //    EndDialogue();
        //}
        //else
        //{
        //    DisplayNextLine();
        //}
        if (currentLine <  dialogueLines.Length )
        {
            DisplayNextLine();
        }else
            EndDialogue();
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        GameManager.instance.dialogueActive = false;
        DeactivateUIInstructions();
        dialogueLines = null;
        if (shouldMarkQuest)
        {
            shouldMarkQuest = false;
            if (markQuestComplete)
            {
                QuestManager.instance.MarkQuestComplete(questToMark);
            }
            else
            {
                QuestManager.instance.MarkQuestIncomplete(questToMark);
            }
        }
    }

    private void DisplayNextLine()
    {
        CheckIfName();
        dialogueText.text = dialogueLines[currentLine];
    }
}
