using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private float translationSpeed;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private int bulletVerticalDirection;

    [SerializeField]
    private float bulletTranslationSpeed;

    [SerializeField]
    private float bulletIntervalGeneration;

    [SerializeField]
    private int lives;

    private float _leftLimit = -7;
    private float _rightLimit = 7;
    private int _direction = 1;

    private void Start()
    {
        InvokeRepeating(nameof(Shoot), bulletIntervalGeneration, bulletIntervalGeneration);
    }

    void Update()
    {
        if (transform.position.x >= _rightLimit)
        {
            _direction = -1;
        }

        if (transform.position.x <= _leftLimit)
        {
            _direction = 1;
        }

        Vector3 translation = new Vector3(_direction, 0, 0) * translationSpeed * Time.deltaTime;
        transform.position += translation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag == "Player")
        {
            lives--;
            if (lives == 0) Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        GameObject bullet1 = Instantiate(bulletPrefab);
        GameObject bullet2 = Instantiate(bulletPrefab);

        BulletController bulletController = bullet.GetComponent<BulletController>();
        BulletController bulletController1 = bullet1.GetComponent<BulletController>();
        BulletController bulletController2 = bullet2.GetComponent<BulletController>();

        // Config bullet vertical direction
        bulletController.verticalDirection = bulletVerticalDirection;
        bulletController1.verticalDirection = bulletVerticalDirection;
        bulletController2.verticalDirection = bulletVerticalDirection;

        // Config bullet velocity
        bulletController.translationSpeed = bulletTranslationSpeed;
        bulletController1.translationSpeed = bulletTranslationSpeed;
        bulletController2.translationSpeed = bulletTranslationSpeed;

        // Config bullet position
        Vector3 playerPosition = transform.position;
        Vector3 position = new Vector3(playerPosition.x, playerPosition.y - 1, 0);
        bullet.transform.position = position;
        Destroy(bullet, 3);

        Vector3 playerPosition1 = transform.position - new Vector3(2.5f, 0, 0);
        Vector3 position1 = new Vector3(playerPosition1.x, playerPosition1.y - 1, 0);
        bullet1.transform.position = position1;
        Destroy(bullet1, 3);

        Vector3 playerPosition2 = transform.position + new Vector3(2.5f, 0, 0);
        Vector3 position2 = new Vector3(playerPosition2.x, playerPosition2.y - 1, 0);
        bullet2.transform.position = position2;
        Destroy(bullet2, 3);
    }
}
