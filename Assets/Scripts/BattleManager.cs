using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using Unity.VisualScripting;
using System.Net;
public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    bool battleActive;

    public GameObject battleScene;

    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    public BattleChar[] playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers = new List<BattleChar>();

    public int currentTurn;
    public bool turnWaiting;

    public GameObject uiButtonHolder;

    public BattleMove[] moveList;
    public GameObject enemyAttackEffect;

    public DamageNumber damageNumber;

    public TMP_Text[] playerName, playerHP, playerMP;

    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;

    public GameObject magicMenu;

    public BattleMagicSelect[] magicButtons;

    public BattleNotification battleNotice;

    public int chanceToFlee = 50;
    bool fleeing;
    public bool cannotFlee;

    public GameObject itemWindow, itemCharChoiceMenu;
    public TMP_Text[] itemCharChoiceNames;
    public Item activeItem;
    public ItemButton[] itemButtons;
    public TMP_Text itemName, itemDescription, useButtonText;


    public string gameOverScene;
    public int rewardXP;
    public string[] rewardItems;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            BattleStart(new string[] { "EyeBall"/*, "Spider", "Skeleton"*/}, false);
        }

        if (battleActive)
        {
            if(turnWaiting)
            {
                if (activeBattlers[currentTurn].isPlayer)
                {
                    uiButtonHolder.SetActive(true);
                }
                else
                {
                    uiButtonHolder.SetActive(false);
                    //enemy should attack
                    StartCoroutine(EnemyMoveCo());

                }
            }

            if(Input.GetKeyDown(KeyCode.N))
            {
                NextTurn();
            }
        }
    }

    public void BattleStart(string[] enemiesToSpawn, bool setCannotFlee)
    {
        if(!battleActive)
        {
            cannotFlee = setCannotFlee;
            battleActive = true;

            //this line makes the player not move while in combat
            GameManager.instance.battleActive = true;


            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            battleScene.SetActive(true);

            AudioManager.instance.PlayBGM(0);

            
            for(int i = 0; i < playerPositions.Length; i++)
            {
                if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy) 
                {
                   
                    for(int j = 0; j < playerPrefabs.Length; j++)
                    {
                        if (playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
                            newPlayer.transform.parent = playerPositions[i];

                            activeBattlers.Add(newPlayer);
                            CharStats thePlayer = GameManager.instance.playerStats[i];
                            activeBattlers[i].currentHP = thePlayer.currentHp;
                            activeBattlers[i].maxHP = thePlayer.maxHP;
                            activeBattlers[i].currentMP = thePlayer.currentMP;
                            activeBattlers[i].maxMP = thePlayer.maxMP;
                            activeBattlers[i].strength = thePlayer.strength;
                            activeBattlers[i].defence = thePlayer.defence;
                            activeBattlers[i].wpnPower = thePlayer.wpnPwr;
                            activeBattlers[i].armrPower = thePlayer.armrPwr;
                        }
                    } 
                }
            }

            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {
                if (enemiesToSpawn[i] != "")
                {
                    for(int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemiesToSpawn[i])
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);
                            newEnemy.transform.parent = enemyPositions[i];
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }

            turnWaiting = true;
            currentTurn = Random.Range(0,activeBattlers.Count);
            //set above int to 0 to have it start at player 1 each time

            UpdateUIStats();
        }
    }

    public void NextTurn()
    {
        currentTurn++;
        if(currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
        }

        turnWaiting = true;
        UpdateBattle();
        UpdateUIStats();
    }

    public void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayersDead = true;

        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].currentHP < 0)
            {
                activeBattlers[i].currentHP = 0;
            }
            if (activeBattlers[i].currentHP == 0)
            {
                //Handle dead battler
                if (activeBattlers[i].isPlayer)
                {
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].deadSprite;

                }
                else
                {
                    activeBattlers[i].EnemyFade();
                }
            }else
            {
                if (activeBattlers[i].isPlayer)
                {
                    allPlayersDead = false;
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].aliveSprite;
                }
                else
                {
                    allEnemiesDead = false;
                }
            }
        }

        if(allEnemiesDead || allPlayersDead)
        {
            if(allEnemiesDead)
            {
                //end battle in victory
                StartCoroutine(EndBattleCo());

            }else
            {
                //end battle in failure
                StartCoroutine(GameOverCo());

            }
            /*battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            battleActive = false;*/
        }else
        {
            while (activeBattlers[currentTurn].currentHP == 0)
            {
                currentTurn++;
                if(currentTurn >= activeBattlers.Count)
                {
                    currentTurn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void EnemyAttack() 
    { 
        List<int> players = new List<int>();
        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].isPlayer && activeBattlers[i].currentHP > 0)
            {
                players.Add(i);
            }
        }
        int selectedTarget = players[Random.Range(0, players.Count)];

        //activeBattlers[selectedTarget].currentHP -= 20;

        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvailable.Length);
        int movePower = 0;
        for(int i = 0; i < moveList.Length - 1; i++)
        {
            if (moveList[i].moveName == activeBattlers[currentTurn].movesAvailable[selectAttack])
            {
                Instantiate(moveList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = moveList[i].movePower;
            }

        }
        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

        DealDamage(selectedTarget, movePower);
    }
    public void DealDamage(int target, int movePower)
    {
        float atkPwr = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].wpnPower;
        float defPwr = activeBattlers[target].defence + activeBattlers[target].armrPower;

        float damageCalc = (atkPwr / defPwr) * movePower * Random.Range(.9f, 1.1f);
        int damageToGive = Mathf.RoundToInt(damageCalc);

        //Debug.Log(activeBattlers[currentTurn].charName + " is dealing" + damageCalc + " (" + damageToGive + " ) damage to " + activeBattlers[target].charName);

        activeBattlers[target].currentHP -= damageToGive;

        Instantiate(damageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive);

        UpdateUIStats();
    }

    public void UpdateUIStats()
    {
        for (int i = 0; i < playerName.Length; i++)
        {
            if(activeBattlers.Count > i)
            {
                if (activeBattlers[i].isPlayer)
                {
                    BattleChar playerData = activeBattlers[i];

                    playerName[i].gameObject.SetActive(true);
                    playerName[i].text = playerData.charName;
                    playerHP[i].text = Mathf.Clamp(playerData.currentHP, 0, int.MaxValue) + "/" + playerData.maxHP;
                    playerMP[i].text = Mathf.Clamp(playerData.currentMP, 0, int.MaxValue) + "/" + playerData.maxMP;

                }else
                    playerName[i].gameObject.SetActive(false);

            }else
                playerName[i].gameObject.SetActive(false);
        }
    }

    public void PlayerAttack(string moveName, int selectedTarget)
    {
        int movePower = 0;
        for (int i = 0; i < moveList.Length - 1; i++)
        {
            if (moveList[i].moveName == moveName)
            {
                Instantiate(moveList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = moveList[i].movePower;
            }
        }
        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        DealDamage(selectedTarget, movePower);
        uiButtonHolder.SetActive(false);

        targetMenu.SetActive(false);

        NextTurn();
    }

    public void OpenTargetMenu(string moveName)
    {
        targetMenu.SetActive(true);

        List<int> Enemies = new List<int>();

        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (!activeBattlers[i].isPlayer)
            {
                Enemies.Add(i);
            }
        }

        for (int i = 0; i < targetButtons.Length; i++)
        {
            if(Enemies.Count > i && activeBattlers[Enemies[i]].currentHP > 0)
            {
                targetButtons[i].gameObject.SetActive(true );
                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattlerTarget = Enemies[i];
                targetButtons[i].targetName.text = activeBattlers[Enemies[i]].charName;

            }else
                targetButtons[i].gameObject.SetActive(false);
        }
    }

    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);

        for (int i = 0; i < magicButtons.Length; i++)
        {
            if (activeBattlers[currentTurn].movesAvailable.Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);
                magicButtons[i].spellName = activeBattlers[currentTurn].movesAvailable[i];
                magicButtons[i].nameText.text = magicButtons[i].spellName;

                for (int j = 0; j < moveList.Length; j++)
                {
                    if (moveList[j].moveName == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = moveList[j].moveCost;
                        magicButtons[i].costText.text = magicButtons[i].spellCost.ToString();
                    }
                }
            }
            else
                magicButtons[i].gameObject.SetActive(false);
        }

    }

    public void Flee()
    {
        if(cannotFlee)
        {
            battleNotice.theText.text = "Cannot flee this battle!";
            battleNotice.Activate();
            
        }else
        {
            int fleeSuccess = Random.Range(0, 100);
            if (fleeSuccess < chanceToFlee)
            {
                //end the battle
                //battleActive = false;
                //battleScene.SetActive(false);
                //GameManager.instance.battleActive = false;
                fleeing = true;
                StartCoroutine(EndBattleCo());
            }
            else
            {
                NextTurn();
                battleNotice.theText.text = "Couln't Escape!";
                battleNotice.Activate();
            }
        }
        
    }

    public void OpenItemMenu()
    {
        itemWindow.SetActive(true);
    }
    public void CloseItemMenu()
    {
        itemWindow.SetActive(false);

    }
    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }
    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);
        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);

        UpdateUIStats();
        CloseItemCharChoice();
        CloseItemMenu();
    }
    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);

    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if (activeItem.isItem)
            useButtonText.text = "Use";

        if (activeItem.isWeapon || activeItem.isArmor)
            useButtonText.text = "Equip";

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public IEnumerator EndBattleCo() 
    { 
        battleActive = false;
        uiButtonHolder.SetActive(false);
        targetMenu.SetActive(false);
        magicMenu.SetActive(false);
        itemWindow.SetActive(false);


        yield return new WaitForSeconds(.5f);

        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);

        for ( int i = 0; i < activeBattlers.Count; i++ )
        {
            if (activeBattlers[i].isPlayer)
            {
                for ( int j = 0; j < GameManager.instance.playerStats.Length; j++ )
                {
                    if (activeBattlers[i].charName == GameManager.instance.playerStats[j].charName)
                    {
                        GameManager.instance.playerStats[j].currentHp = activeBattlers[i].currentHP;
                        GameManager.instance.playerStats[j].currentMP = activeBattlers[i].currentMP;

                        //any other stat changes.
                    }
                }
            }
            Destroy(activeBattlers[i].gameObject);
        }
        UIFade.instance.FadeFromBlack();
        battleScene.SetActive(false);
        activeBattlers.Clear();
        currentTurn = 0;
        if (fleeing)
        {
            GameManager.instance.battleActive = false;
            fleeing = false;
        }
        else
        {
            BattleReward.Instance.OpenRewardScreen(rewardXP, rewardItems);
        }

        AudioManager.instance.PlayBGM(FindObjectOfType<CameraController>().musicToPlay);
    }

    public IEnumerator GameOverCo()
    {
        battleActive = false;
        UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(gameOverScene);
    }
}
