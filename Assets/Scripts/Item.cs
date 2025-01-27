using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("-----Item Type-----")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("-----Item Details-----")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;
    public int amountToChange;
    public bool affectHP, affectMP, affectStr, affectDef;

    [Header("---Weapon/Armor Details---")]
    public int weaponStrength;
    public int armorStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int charToUseOn)
    {

        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];
        if (GameManager.instance.battleActive)
        {
            BattleChar selectedBattleChar = BattleManager.instance.activeBattlers[charToUseOn];
                if (isItem)
                {
                    if (affectHP)
                    {
                        selectedBattleChar.currentHP += amountToChange;
                        if (selectedBattleChar.currentHP > selectedBattleChar.maxHP)
                        {
                            selectedBattleChar.currentHP = selectedBattleChar.maxHP;
                        }
                    }
                    if (affectMP)
                    {
                        selectedBattleChar.currentMP += amountToChange;
                        if (selectedBattleChar.currentMP > selectedBattleChar.maxMP)
                        {
                            selectedBattleChar.currentMP = selectedBattleChar.maxMP;
                        }
                    }
                    if (affectStr)
                    {
                        selectedBattleChar.strength += amountToChange;
                    }
                    if (affectDef)
                    {
                        selectedBattleChar.defence += amountToChange;
                    }
                }

                if (isWeapon)
                {
                    if (selectedBattleChar.wpnPower != 0)
                    {
                        GameManager.instance.AddItem(selectedChar.equippedWepn);
                    }
                    selectedChar.equippedWepn = itemName;
                    selectedChar.wpnPwr = weaponStrength;
                    selectedBattleChar.wpnPower = weaponStrength;
                }

                if (isArmor)
                {
                    if (selectedBattleChar.armrPower != 0)
                    {
                        GameManager.instance.AddItem(selectedChar.equippedArmr);
                    }

                    selectedChar.equippedArmr = itemName;
                    selectedChar.armrPwr = armorStrength;
                    selectedBattleChar.armrPower = armorStrength;
                }

                GameManager.instance.RemoveItem(itemName);
            }
        else
        {
            if (isItem)
            {
                if (affectHP)
                {
                    selectedChar.currentHp += amountToChange;
                    if (selectedChar.currentHp > selectedChar.maxHP)
                    {
                        selectedChar.currentHp = selectedChar.maxHP;
                    }
                }
                if (affectMP)
                {
                    selectedChar.currentMP += amountToChange;
                    if (selectedChar.currentMP > selectedChar.maxMP)
                    {
                        selectedChar.currentMP = selectedChar.maxMP;
                    }
                }
                if (affectStr)
                {
                    selectedChar.strength += amountToChange;
                }
                if (affectDef)
                {
                    selectedChar.defence += amountToChange;
                }
            }

            if (isWeapon)
            {
                if (selectedChar.equippedWepn != "")
                {
                    GameManager.instance.AddItem(selectedChar.equippedWepn);
                }

                selectedChar.equippedWepn = itemName;
                selectedChar.wpnPwr = weaponStrength;
            }

            if (isArmor)
            {
                if (selectedChar.equippedArmr != "")
                {
                    GameManager.instance.AddItem(selectedChar.equippedArmr);
                }

                selectedChar.equippedArmr = itemName;
                selectedChar.armrPwr = armorStrength;
            }

            GameManager.instance.RemoveItem(itemName);
        }
      
    }
}
