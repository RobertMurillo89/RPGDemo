using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogueActive, fadingBetweenAreas;



    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(gameMenuOpen || dialogueActive || fadingBetweenAreas)
            Player.instance.canMove = false;
        else
            Player.instance.canMove = true;
    }
}
