using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunFunctions : MonoBehaviour
{
    //Need to implement bullet destruction when colliding with walls
    public GameObject bulletPrefab;
    public Transform barrel;
    public float reloadSpeed = 2f;
    public float bulletSpeed = 40.0f;
    public float burstSpeed = .1f;
    public float damage = 15f;

    public bool isReloading;
    public GameObject sightline;

    //Shooting sounds
    public AudioSource source;
    public AudioClip shootingSound;

    void Start()
    {
        isReloading = false;
    }


    void Update()
    {
        bool canSeePlayer = GetComponent<FieldOfView>().canSeeTarget;
        if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }

        if(EventBus.Instance.inAlertPhase == false)
        {
            return;
        }

        if(canSeePlayer == true && isReloading == false)
        {
            StartCoroutine(Shoot3());
            isReloading = true;
            
        }
    }

    IEnumerator Shoot3()
    {
        source.PlayOneShot(shootingSound, .65f);
        Shoot();
        yield return new WaitForSeconds(burstSpeed);
        source.PlayOneShot(shootingSound, .65f);
        Shoot();
        yield return new WaitForSeconds(burstSpeed);
        source.PlayOneShot(shootingSound, .65f);
        Shoot();
        yield return new WaitForSeconds(reloadSpeed);
        isReloading = false;
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;
        bulletRigidbody.velocity = barrel.forward * bulletSpeed;
        StartCoroutine(BulletLife(5, newBullet));
    }
    
    IEnumerator BulletLife(float timer, GameObject newBullet)
    {
        yield return new WaitForSeconds(timer);
        Destroy(newBullet);
    }


}
