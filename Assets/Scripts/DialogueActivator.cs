using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public string[] lines;
    public string name;

    private bool canActivate;

    public bool isPerson = true;

    public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;

    void Update()
    {
        if(canActivate && Input.GetKeyDown(KeyCode.E) && !DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            DialogueManager.instance.ShowDialogue(lines, isPerson);
            DialogueManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            DialogueManager.instance.ActivateUIInstruction();
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DialogueManager.instance.DeactivateUIInstructions();
            canActivate = false;
        }
    }
}
