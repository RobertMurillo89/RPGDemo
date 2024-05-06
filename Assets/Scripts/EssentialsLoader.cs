using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{

    public GameObject UIScreen;
    public GameObject player;

    void Start()
    {
        if (Player.instance == null)
        {
            Player clone = Instantiate(player).GetComponent<Player>();
            Player.instance = clone;
        }

        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }
    }

}
