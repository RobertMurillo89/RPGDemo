using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;
    public string areaTransitionName;
    public AreaEntrance theEntrance;

    public float waitToLoad = .1f;
    private bool shouldLoadAterFade;

    private void Start()
    {
        theEntrance.transitionName = areaTransitionName;
    }

    private void Update()
    {
        if (shouldLoadAterFade)
        {
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0)
            {
                shouldLoadAterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.LoadScene(areaToLoad);
            shouldLoadAterFade = true;
            UIFade.instance.FadeToBlack();
            Player.instance.areaTransitionName = areaTransitionName;
        }
    }
}
