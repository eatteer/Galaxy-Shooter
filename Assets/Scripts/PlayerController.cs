using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementVelocity;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private int bulletVerticalDirection;

    [SerializeField]
    private float bulletVelocity;

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Meteor")
        {
            Debug.Log("Meteor collision");
        }
        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy collision");
        }
        if (collision.tag == "Bullet")
        {
            Debug.Log("Bullet collision");
        }
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(inputX, inputY, 0) * movementVelocity * Time.deltaTime;
        this.transform.Translate(movement);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        // Config bullet vertical direction
        bulletController.verticalDirection = bulletVerticalDirection;

        // Config bullet velocity
        bulletController.velocity = bulletVelocity;

        // Config bullet position
        Vector3 playerPosition = this.transform.position;
        Vector3 position = new Vector3(playerPosition.x, playerPosition.y + 1, 0);
        bullet.transform.position = position;
    }
}
