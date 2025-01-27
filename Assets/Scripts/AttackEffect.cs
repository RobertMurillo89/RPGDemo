using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public float effectLength;
    public int soundEffect;

    void Start()
    {
        AudioManager.instance.PlaySFX(soundEffect);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, effectLength);
    }
}
