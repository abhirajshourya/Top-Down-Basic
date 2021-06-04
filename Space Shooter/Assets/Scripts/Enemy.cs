using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyHealth = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetShots = 0.2f;
    [SerializeField] float maxTimeBetShots = 3f;
    [SerializeField] GameObject laserPrefab;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] float projectileSpeed;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathSFXVolume = 0.5f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0,1)] float shootSFXVolume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetShots, maxTimeBetShots);    
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();    
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetShots, maxTimeBetShots);
        }
    }

    private void Fire()
    {
        var newPos = new Vector2(transform.position.x, transform.position.y-1);
        GameObject enemyLaser = Instantiate(laserPrefab, newPos, Quaternion.identity) as GameObject;
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) {   return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        enemyHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
