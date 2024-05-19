using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleMagicSelect : MonoBehaviour
{
    public string spellName;
    public int spellCost;
    public TMP_Text nameText;
    public TMP_Text costText;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Press()
    {
        if (BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP >= spellCost)
        {
            BattleManager.instance.magicMenu.SetActive(false);
            BattleManager.instance.OpenTargetMenu(spellName);
            BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP -= spellCost;
        }
        else
        {
            //let player know there is not enough mp
        }

    }
}
