using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class EventBus
{
    // elapsed_time = timer.ElapsedMilliseconds;
    // Format and display the TimeSpan value.
    //   string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
    public Stopwatch timer = new Stopwatch();
    public bool canMove = true;
    public bool enemyCanMove = true;
    public bool hasMacguffin = false;
    public bool playerisSeen = false;
    public bool inAlertPhase = false;
    public Vector3 playerLastKnownPosition;
    public float timeElapsed = 0;
    public int numTimesAlertPhaseEntered = 0;
    public int numKilledEnemies = 0;
    public static EventBus Instance { get { if (theInstance == null) theInstance = new EventBus(); return theInstance; } }

    static EventBus theInstance;

    public event Action onGameStart;
    public event Action onGameEnd;
    public event Action onOpenInventory;
    public event Action onCloseInventory;

    public event Action<ItemData> onPickUpItem;

    public event Action onPickUpMacguffin;
    public event Action onPlayerIsSeen;
    public event Action onPlayerIsHidden;
    public event Action onEnterAlertPhase;
    public event Action onExitAlertPhase;

    public event Action onEnemyKilled;

    public event Action onAnimationStart;

    public event Action onAnimationEnd;

    public event Action onLevelLoadStart;

    public event Action onLevelLoadEnd;

    public void GameStart() {
        timer.Start();
    }

    public void GameEnd() {
        timer.Stop();
    }
    
    public void OpenInventory()
    {
        onOpenInventory?.Invoke();
        canMove = false;
        enemyCanMove = false;
    }

    
    public void CloseInventory()
    {
        onCloseInventory?.Invoke();
        canMove = true;
        enemyCanMove = true;
    }

    public void PickUpItem(ItemData itemData)
    {
        //When function is invoked, it takes the item data.
        onPickUpItem?.Invoke(itemData);
    }

    public void PickUpMacguffin()
    {
        onPickUpMacguffin?.Invoke();
        hasMacguffin = true;
    }

    public void PlayerIsSeen(Vector3 playerCurrentPosition)
    {
        playerLastKnownPosition = playerCurrentPosition;
        onPlayerIsSeen?.Invoke();
        playerisSeen = true;
        EnterAlertPhase();
    }
    public void PlayerIsHidden()
    {
        onPlayerIsHidden?.Invoke();
        playerisSeen = false;
    }
    public void EnterAlertPhase()
    {
        onEnterAlertPhase?.Invoke();
        inAlertPhase = true;
        numTimesAlertPhaseEntered++;
    }

    public void ExitAlertPhase()
    {
        onExitAlertPhase?.Invoke();
        inAlertPhase = false;
    }
    public void EnemyKilled()
    {
        numKilledEnemies++;
    }

    public void AnimationStart()
    {
        onAnimationStart?.Invoke();
        canMove = false;
    }
    public void AnimationEnd()
    {
        onAnimationEnd?.Invoke();
        canMove = true;
    }
    public void LevelLoadStart()
    {
        onLevelLoadStart?.Invoke();
        canMove = false;
        enemyCanMove = false;
    }
    public void LevelLoadEnd()
    {
        onLevelLoadEnd?.Invoke();
        canMove = true;
        enemyCanMove = true;
    }


}
