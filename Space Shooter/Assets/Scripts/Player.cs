using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //cofig params
    [Header("Player Movement")]
    float playerSpeed = 10f;
    float xMin, yMin, xMax, yMax, padding = 1f;
    [SerializeField] float playerHP = 200f;
    [SerializeField] float projectileTime = 0.05f;
    
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 15f;
    Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpBoundaries();
    }  

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }
    private void SetUpBoundaries()
    {
        Camera mainCamera = Camera.main;
        xMin = mainCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = mainCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = mainCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
        yMax = mainCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;
    }

    private void Move()
    {      
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
        var newPos = new Vector2(transform.position.x, transform.position.y+1);
        GameObject laser = Instantiate(laserPrefab, newPos, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        yield return new WaitForSeconds(projectileTime);
        }
    }
    private void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) {   return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        playerHP -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (playerHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
