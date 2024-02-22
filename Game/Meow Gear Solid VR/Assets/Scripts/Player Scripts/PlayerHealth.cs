using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private GameObject bloodSplat;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Animator animator;
    private GameObject splatter;
    public Transform playerHead;
    public bool isDead;
    
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvulnerable;
    public Renderer player;
    public HealthBar healthBar;
    public VideoFader fader;
    [SerializeField] private GameObject GameOverScreen;

    public float MaxHealth{
        get { return MaxHealth; }
    }
    public float CurrentHealth{
        get { return CurrentHealth; }
        
    }
    void Start(){
        currentHealth = maxHealth;
        healthBar = GameObject.FindWithTag("Healthbar").GetComponent<HealthBar>();
        player = GameObject.FindWithTag("GameOverVideoPlayer").GetComponent<Renderer>();
        fader = GameObject.FindWithTag("fader").GetComponent<VideoFader>();
        player = GetComponentInChildren<SkinnedMeshRenderer>();
        player.enabled = true;
        isInvulnerable = false;
        isDead = false;
        healthBar.SetHealth(currentHealth);
        GameOverScreen.SetActive(false);

    }
    public void TakeDamage(float damageAmount)
    {
        // Debug.Log("TakeDamage(): " + isInvulnerable);
        if(isInvulnerable == false)
        {
            StartCoroutine("GetInvulnerable");
            currentHealth -= damageAmount;
            splatter = Instantiate(bloodSplat, playerHead, false);
            StartCoroutine(BloodTimer(splatter));
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0){
                onDeath();
            }
        }
    }

    public void HealHealth(float healAmount){
        if(currentHealth + healAmount > maxHealth){
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
        else{
            currentHealth += healAmount;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void onDeath()
    {
        StartCoroutine(DeathTimer(playerModel));
        float timer = 2f;
        fader.FadeToBlack(timer);
        Debug.Log("GAME OVER");
        

    }

    void Update()
    {//tests our damage function. Must remove later
		//if (Input.GetKeyDown(KeyCode.G))
		//{
		//	TakeDamage(100);
		//}
        //if (Input.GetKeyDown(KeyCode.H))
		//{
		//	HealHealth(20);
		//}
    }
    IEnumerator GetInvulnerable()
    {
        isInvulnerable = true;
        StartCoroutine("FlashColor");
        yield return new WaitForSeconds(2f);
        StopCoroutine("FlashColor");
        isInvulnerable = false;
    }
    IEnumerator FlashColor()
    {
        Color invisible;
        invisible = player.material.color;
        int x = 0;
        while(x <= 10)
        {
            invisible.a = .25f;
            player.material.color = invisible;
            yield return new WaitForSeconds(.25f);
            invisible.a = 1f;
            player.material.color = invisible;
            yield return new WaitForSeconds(.25f);
            x++;
        }
    }
    IEnumerator DeathTimer(GameObject player)
    {
        EventBus.Instance.LevelLoadStart();
        isDead = true;
        animator.SetBool("IsDead", isDead);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("FlashColor");
        yield return new WaitForSeconds(1.5f);
        GameOverScreen.SetActive(true);
        Destroy(player);
    }
    IEnumerator BloodTimer(GameObject splatter)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(splatter);
    }

    
}
