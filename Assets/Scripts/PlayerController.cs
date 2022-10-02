using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int lives;

    [SerializeField]
    private float translationSpeed;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private int bulletVerticalDirection;

    [SerializeField]
    private float bulletTranslationSpeed;

    void Update()
    {
        if (lives == 0)
        {
            Time.timeScale = 0;
            Destroy(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Meteor" || collision.tag == "Enemy" || collision.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            this.lives--;
        }
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 translation = new Vector3(inputX, inputY, 0) * translationSpeed * Time.deltaTime;
        Vector3 nextPosition = this.transform.position + translation;
        float clampedX = Mathf.Clamp(nextPosition.x, -8f, 8f);
        float clampedY = Mathf.Clamp(nextPosition.y, -4f, 4f);
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, 0);
        this.transform.position = clampedPosition;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        // Config bullet vertical direction
        bulletController.verticalDirection = bulletVerticalDirection;

        // Config bullet velocity
        bulletController.translationSpeed = bulletTranslationSpeed;

        // Config bullet position
        Vector3 playerPosition = this.transform.position;
        Vector3 position = new Vector3(playerPosition.x, playerPosition.y + 1, 0);
        bullet.transform.position = position;

        Destroy(bullet, 3);
    }
}
