using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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

    private float _leftLimit = -8;
    private float _rightLimit = 8;
    private int _direction = 1;

    private void Start()
    {
        InvokeRepeating(nameof(Shoot), bulletIntervalGeneration, bulletIntervalGeneration);
    }

    void Update()
    {
        if (this.transform.position.x >= _rightLimit)
        {
            _direction = -1;
        }

        if (this.transform.position.x <= _leftLimit)
        {
            _direction = 1;
        }

        Vector3 translation = new Vector3(_direction, 0, 0) * translationSpeed * Time.deltaTime;
        this.transform.Translate(translation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
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
        Vector3 position = new Vector3(playerPosition.x, playerPosition.y - 1, 0);
        bullet.transform.position = position;

        Destroy(bullet, 3);
    }
}
