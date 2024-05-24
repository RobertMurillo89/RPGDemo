using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{

    public GameObject objectToActivate;

    public string questToCheck;

    public bool activeIfComplete;

    bool initialCheckDone;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!initialCheckDone)
        {
            initialCheckDone = true;

            CheckCompletion();
        }
    }

    public void CheckCompletion()
    {
        if (QuestManager.instance.CheckIfComplete(questToCheck))
        {
            //QuestManager.instance.questObjectAlreadyUsed = true;
            objectToActivate.SetActive(activeIfComplete);

        }
    }

    
}
