using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class AlertPhase : MonoBehaviour
{
    //These lines are for Danger Warning during Alert Phase
    public GameObject miniMap;
    public GameObject AlertInfo;
    public TextMeshProUGUI TimerText;
    private double timeRemaining = 0.00;
    public double alertDuration = 15;
    private Transform player;
    public bool inAlertPhase;
    public Vector3 lastKnownPosition; 
    public bool playerIsSeen;

    //These lines are for playing music
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip alertTheme;
    public bool alertMusicPlaying;



    // Start is called before the first frame update
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        musicSource = GetComponent<AudioSource>();
        alertMusicPlaying = false;
        timeRemaining = alertDuration;
        AlertInfo.SetActive(false);
        miniMap.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        playerIsSeen = EventBus.Instance.playerisSeen;
        inAlertPhase = EventBus.Instance.inAlertPhase;
        if (inAlertPhase == true) 
        {
            if (timeRemaining <= 0)
            {
                //Exit AlertPhase
                timeRemaining = 0;
                inAlertPhase = false;
                miniMap.SetActive(true);
                AlertInfo.SetActive(false);
                EventBus.Instance.ExitAlertPhase();
                timeRemaining = alertDuration;
                StartCoroutine(FadeOut(musicSource, 3));
                return;
            }
            if (EventBus.Instance.playerisSeen == true)
            {
                lastKnownPosition = player.position;
                timeRemaining = alertDuration;
                TimerText.text = string.Format("{0:00}", timeRemaining);
                AlertInfo.SetActive(true);
                miniMap.SetActive(false);
            }
            else
            {
                timeRemaining -= Time.deltaTime;
                TimerText.text = string.Format("{0:00}", timeRemaining);
                AlertInfo.SetActive(true);
                miniMap.SetActive(false);
            }

        }
        if (inAlertPhase == true && alertMusicPlaying == false)
        {
            StartCoroutine ("PlayAlertTheme");
        }
        
    }
    IEnumerator PlayAlertTheme()
    {
                alertMusicPlaying = true;
                musicSource.Stop();
                Debug.Log("Old Music Stopped");
                musicSource.clip = alertTheme;
                Debug.Log("Music Switched");
                musicSource.Play();
                Debug.Log("Music Started");
                yield return new WaitForSeconds (musicSource.clip.length);
    }
 
    IEnumerator FadeOut(AudioSource audioSource, float FadeTime) 
    {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop();
        audioSource.volume = startVolume;
        StopCoroutine ("PlayAlertTheme");
        alertMusicPlaying = false;
    }
 
}
