using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePersist : MonoBehaviour
{
    void OnDisable() => Save();

    void OnEnable() => Load();

    void Load()
    {
        foreach (var persist in FindObjectsOfType<MonoBehaviour>(true).OfType<IPersist>())
        {
            persist.Load();
        }
    }

    void Save()
    {
        foreach (var persist in FindObjectsOfType<MonoBehaviour>(true).OfType<IPersist>())
        {
            persist.Save();
        }
    }
}

internal interface IPersist
{
    void Save();
    void Load();
}


