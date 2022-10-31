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

    private GameObject explosionAudio;

    private float _leftLimit = -8;
    private float _rightLimit = 8;
    private int _direction = 1;

    private void Start()
    {
        InvokeRepeating(nameof(Shoot), bulletIntervalGeneration, bulletIntervalGeneration);
        explosionAudio = transform.Find("ExplosionAudio").gameObject;
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
        transform.Translate(translation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag == "Player")
        {
            translationSpeed = 0;
            explosionAudio.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            CancelInvoke();
            Destroy(gameObject, 2);
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
        Vector3 playerPosition = transform.position;
        Vector3 position = new Vector3(playerPosition.x, playerPosition.y - 1, 0);
        bullet.transform.position = position;

        Destroy(bullet, 3);
    }
}
