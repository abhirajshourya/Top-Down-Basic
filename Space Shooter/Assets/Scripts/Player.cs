using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //cofig params
    float playerSpeed = 10f;
    float playerSpeedMouse = 10f;
    float xMin, yMin, xMax, yMax, padding = 1f;
    
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MovewithMouse();
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

    private void MovewithMouse()
    {      
        var deltaX = Input.GetAxis("Mouse X") * Time.deltaTime * playerSpeedMouse;
        var deltaY = Input.GetAxis("Mouse Y") * Time.deltaTime * playerSpeedMouse;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }
}
