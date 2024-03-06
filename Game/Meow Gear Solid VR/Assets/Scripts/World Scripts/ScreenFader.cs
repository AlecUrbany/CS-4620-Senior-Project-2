using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image faderOverlay;

    private bool busy;

    public void FadeToBlack(float duration)
    {
        if (busy)
        {
            return;
        }
        StartCoroutine(FadeToBlackCount(duration));
    }
    
    public void FadeFromBlack(float duration)
    {
        if (busy)
        {
            return;
        }
        StartCoroutine(FadeFromBlackCount(duration));
    }
    private IEnumerator FadeToBlackCount(float duration)
    {
        busy = true;
        while(faderOverlay.color.a < 1)
        {
            faderOverlay.color = new Color(0,0,0, faderOverlay.color.a + (Time.deltaTime/duration));
            yield return null;
        }
        
        faderOverlay.color = new Color(0,0,0,1);
        busy = false;
        yield return null;
    }
    private IEnumerator FadeFromBlackCount(float duration)
    {
        busy = true;
        while(faderOverlay.color.a > 0)
        {
            faderOverlay.color = new Color(0,0,0, faderOverlay.color.a - (Time.deltaTime/duration));
            yield return null;
        }
        faderOverlay.color = new Color(0,0,0,0);
        busy = false;
        yield return null;
    }
}
