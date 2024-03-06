using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ScreenFader fader;
    public void StartGame ()
    {
        Debug.Log("Loading Next Level");
        float timer = 2;
        EventBus.Instance.LevelLoadStart();
        EventBus.Instance.GameStart();
        StartCoroutine(Delay(timer));
    }
    public void QuitGame ()
    {
        Debug.Log("Program Terminated");
        Application.Quit();
    }
    private IEnumerator Delay(float duration)
    {
        fader.FadeToBlack(duration);
        yield return new WaitForSeconds(duration);
        Debug.Log("Entering next level");
        fader.FadeFromBlack(duration);
        EventBus.Instance.LevelLoadEnd();
        SceneManager.LoadScene(1);
    }
    
}
