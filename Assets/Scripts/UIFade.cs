using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{

    public static UIFade instance;
    public Image fadeScreen;
    public float fadeSpeed;
    public bool ShouldFadeToBlack;
    public bool ShouldFadeFromBlack;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f )
            {
                ShouldFadeToBlack = false;
            }
        }

        if (ShouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                ShouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        ShouldFadeToBlack = true;
        ShouldFadeFromBlack = false;
        GameMenu.instance.ToggleUIInstructions();
    }

    public void FadeFromBlack()
    {
        ShouldFadeToBlack = false;
        ShouldFadeFromBlack = true;
        GameMenu.instance.ToggleUIInstructions();
    }
}
