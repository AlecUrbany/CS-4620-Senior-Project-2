using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private GameObject bloodSplat;
    [SerializeField] private GameObject enemyBody;
    private GameObject splatter;
    public Transform enemyHead;
    public Renderer enemy;
    public Animator animator;
    public bool isDead;
    public float maxHealth = 50f;
    public float currentHealth;
    public float MaxHealth{
        get { return MaxHealth; }
    }
    public float CurrentHealth{
        get { return CurrentHealth; }
    }
    void Start(){
        isDead = false;
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damageAmount){
        currentHealth -= damageAmount;
        splatter = Instantiate(bloodSplat, enemyHead, false);
        StartCoroutine(BloodTimer(splatter));
        if(currentHealth <= 0){
            onDeath();
        }
    }

    public void HealHealth(float healAmount){
        if(currentHealth + healAmount > maxHealth){
            currentHealth = maxHealth;
        }
        else{
            currentHealth += healAmount;
        }
    }

    public void onDeath(){
        //gameObject.SetActive(false);
        EventBus.Instance.EnemyKilled();
        StartCoroutine(DeathTimer(enemyBody));
    }
    IEnumerator BloodTimer(GameObject splatter)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(splatter);
    }
    IEnumerator DeathTimer(GameObject enemy)
    {
        isDead = true;
        animator.SetBool("IsDead", isDead);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("FlashColor");
        yield return new WaitForSeconds(1.5f);
        Destroy(enemy);
    }
    IEnumerator FlashColor()
    {
        Color invisible;
        invisible = enemy.material.color;
        int x = 0;
        while(x <= 10)
        {
            invisible.a = .25f;
            enemy.material.color = invisible;
            yield return new WaitForSeconds(.25f);
            invisible.a = 1f;
            enemy.material.color = invisible;
            yield return new WaitForSeconds(.25f);
            x++;
        }
    }
}
