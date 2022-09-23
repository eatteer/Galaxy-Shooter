using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float movementVelocity;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private int bulletVerticalDirection;

    [SerializeField]
    private float bulletVelocity;

    private float leftLimit = -8;
    private float rightLimit = 8;
    private int direction = 1;

    private void Start()
    {
        InvokeRepeating(nameof(Shoot), 1, 1);
    }

    void Update()
    {
        if (this.transform.position.x >= rightLimit)
        {
            direction = -1;
        }

        if (this.transform.position.x <= leftLimit)
        {
            direction = 1;
        }

        Vector3 movement = new Vector3(direction, 0, 0) * movementVelocity * Time.deltaTime;
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
        Vector3 position = new Vector3(playerPosition.x, playerPosition.y - 1, 0);
        bullet.transform.position = position;
    }
}
