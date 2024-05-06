using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public static DialogueManager instance;

    void Start()
    {
        instance = this;

        //dialogueText.text = dialogueLines[currentLine];
    }

    void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!justStarted)
                {
                    currentLine++;
                    if (currentLine >= dialogueLines.Length)
                    {
                        dialogueBox.SetActive(false);
                        Player.instance.canMove = true;
                    }
                    else
                        dialogueText.text = dialogueLines[currentLine];
                }
                else
                    justStarted = false;
                
            }
        }
    }

    public void ShowDialogue(string[] newLines)
    {
        dialogueLines = newLines;

        currentLine = 0;

        dialogueText.text = dialogueLines[0];
        dialogueBox.SetActive (true);

        justStarted = true;

        Player.instance.canMove = false;
    }

}
