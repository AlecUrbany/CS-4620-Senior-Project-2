using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private GameObject bloodSplat;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Animator animator;
    [SerializeField] private Renderer player;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject PlayerDyingModel;
    private GameObject splatEffect;
    public VideoFader fader;
    public Transform playerHead;
    public bool isDead;
    
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvulnerable;
    
    public HealthBar healthBar;
    

    public float MaxHealth{
        get { return MaxHealth; }
    }
    public float CurrentHealth{
        get { return CurrentHealth; }
        
    }
    void Start(){
        currentHealth = maxHealth;
        healthBar = GameObject.FindWithTag("Healthbar").GetComponent<HealthBar>();
        player = GameObject.FindWithTag("Player").GetComponent<Renderer>();
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
            splatEffect = Instantiate(bloodSplat, playerHead, false);
            StartCoroutine(BloodTimer(splatEffect));
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
        //Spawns in player ragdoll
        Instantiate(PlayerDyingModel, playerHead, false);
        //Vanishes actual player model
        Color invisible;
        invisible = player.material.color;
        invisible.a = 0f;
        player.material.color = invisible;
        //StartCoroutine(DeathTimer(playerModel));
        //float timer = 2f;
        //fader.FadeToBlack(timer);
       //Debug.Log("GAME OVER");
        

    }

    void Update()
    {//tests our damage function. Must remove later
		if (Input.GetKeyDown(KeyCode.G))
		{
			TakeDamage(50);
		}
        if (Input.GetKeyDown(KeyCode.H))
		{
			HealHealth(20);
		}
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
    IEnumerator BloodTimer(GameObject splatEffect)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(splatEffect);
    }

    
}
