using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class AlertPhase : MonoBehaviour
{
    public GameObject miniMap;
    public GameObject AlertInfo;
    public TextMeshProUGUI TimerText;
    private double timeRemaining = 0.00;
    public double alertDuration = 15;
    private Transform player;
    public bool inAlertPhase;
    private Vector3 lastKnownPosition; 
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = alertDuration;
        AlertInfo.SetActive(false);
        miniMap.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        inAlertPhase = EventBus.Instance.inAlertPhase;
        if (inAlertPhase == true) 
        {
            if (timeRemaining <= 0)
            {
                // Exit AlertPhase
                timeRemaining = 0;
                inAlertPhase = false;
                miniMap.SetActive(true);
                AlertInfo.SetActive(false);
                EventBus.Instance.ExitAlertPhase();
                timeRemaining = alertDuration;
                return;
            }
            if (EventBus.Instance.playerisSeen == true)
            {
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
    }
}
