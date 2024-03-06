using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject statsPage;
    public ScreenFader fader;
    // Start is called before the first frame update
    void Start()
    {
        statsPage.SetActive(false);
        EventBus.Instance.LevelLoadStart();
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Delay");
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(25);
        StartCoroutine("DelayTwo");
    }
    private IEnumerator DelayTwo()
    {
        fader.FadeToBlack(1.5f);
        EventBus.Instance.LevelLoadEnd();
        yield return new WaitForSeconds(2f);
        statsPage.SetActive(true);
        fader.FadeFromBlack(1f);
        StopAllCoroutines();
    }
}
