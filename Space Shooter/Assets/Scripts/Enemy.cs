using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyHealth = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetShots = 0.2f;
    [SerializeField] float maxTimeBetShots = 3f;
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
        }
    }

    private void Fire()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        PrecessHit(damageDealer);
    }

    private void PrecessHit(DamageDealer damageDealer)
    {
        enemyHealth -= damageDealer.GetDamage();

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
