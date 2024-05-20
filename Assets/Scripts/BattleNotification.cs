using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleNotification : MonoBehaviour
{
    public TMP_Text theText;
    public float awakeTime;
    float awakeCounter;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(awakeCounter > 0)
        {
            awakeCounter -= Time.deltaTime;
            if(awakeCounter <= 0)
                gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        awakeCounter = awakeTime;
    }
}
