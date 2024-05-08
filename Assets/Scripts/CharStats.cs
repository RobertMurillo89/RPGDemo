using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int curLevel;
    public int currentExp;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;

    public int currentHp;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;
    public int[] mpLvlBonus;
    public int strength;
    public int defence;
    public int wpnPwr;
    public int armrPwr;
    public string equippedWepn;
    public string equippedArmr;
    public Sprite charImage;

    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(500);
        }
    }

    public void AddExp(int expToAdd)
    {
        if (curLevel < maxLevel)
        {
            currentExp += expToAdd;

            if (currentExp > expToNextLevel[curLevel]) 
            { 
            currentExp -= expToNextLevel[curLevel];
            curLevel++;

            //determine whether to add str or def based on odd or even
            if (curLevel % 2 == 0) // even number
                strength++;
            else
                defence++;

            maxHP = Mathf.FloorToInt(maxHP * 1.05f);
            currentHp = maxHP;

            maxMP += mpLvlBonus[curLevel];
            currentMP = maxMP;
            }
        }
        if (curLevel >= maxLevel)
            currentExp = 0;
    }

}
