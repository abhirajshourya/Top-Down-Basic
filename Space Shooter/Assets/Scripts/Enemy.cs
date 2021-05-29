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
    [SerializeField] float projectileSpeed;
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
            Destroy(gameObject);
        }
    }
}
