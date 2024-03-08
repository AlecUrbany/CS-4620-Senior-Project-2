using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject startingMenu;
    float previousTimeScale = 1;
    public static bool isPaused;
    public AudioSource source;
    public AudioClip pauseSound;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        if(Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            source.PlayOneShot(pauseSound, .75f);
            isPaused = true;
            pauseMenu.SetActive(true);
            startingMenu.SetActive(true);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            EventBus.Instance.OpenInventory();
        }
        else if (Time.timeScale == 0)
        {
            pauseMenu.SetActive(false);
             startingMenu.SetActive(false);
            optionsMenu.SetActive(false);
            controlsMenu.SetActive(false);
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            isPaused = false;
            EventBus.Instance.CloseInventory();
        }
    }

    public void QuitGame ()
    {
        Debug.Log("Program Terminated");
        Application.Quit();
    }
}
