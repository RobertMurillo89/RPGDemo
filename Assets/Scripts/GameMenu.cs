using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;

    private CharStats[] playerStats;

    public TMP_Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public GameObject[] statusButtons;

    public TMP_Text statusName, statusHP, statusMP, statusStr, statusDef, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, statusArmrPwr, statusExp;
    public Image statusImage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (theMenu.activeInHierarchy)
            {
                CloseMenu();
            }else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHp + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Lvl: " + playerStats[i].curLevel;
                expText[i].text = $"{playerStats[i].currentExp}/{playerStats[i].expToNextLevel[playerStats[i].curLevel]}";
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].curLevel];
                expSlider[i].value = playerStats[i].currentExp;
                charImage[i].sprite = playerStats[i].charImage;
            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();
        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
                windows[i].SetActive(!windows[i].activeInHierarchy);
            else
                windows[i].SetActive(false);
        }
    }

    public void CloseMenu()
    {
        for(int i = 0;i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        theMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;
    }

    public void OpenStatus()
    {
        UpdateMainStats();
        //update the information that is shown
        StatusCharacter(0);
        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<TMP_Text>().text = playerStats[i].charName;
        }
    }

    public void StatusCharacter(int selected)
    {
        statusName.text = playerStats[selected].charName;
        statusHP.text = "" + playerStats[selected].currentHp + "/" + playerStats[selected].maxHP;
        statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStr.text = playerStats[selected].strength.ToString();
        statusDef.text = playerStats[selected].defence.ToString();
        if (playerStats[selected].equippedWepn != "")
            statusWpnEqpd.text = playerStats[selected].equippedWepn;
        statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();
        if (playerStats[selected].equippedArmr != "")
            statusArmrEqpd.text = playerStats[selected].equippedArmr;
        statusArmrPwr.text = playerStats[selected].armrPwr.ToString();
        statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].curLevel] - playerStats[selected].currentExp).ToString();
        statusImage.sprite = playerStats[selected].charImage;

    }
}
