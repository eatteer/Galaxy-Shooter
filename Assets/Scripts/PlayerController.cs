using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int lives;

    [SerializeField]
    private float translationSpeed;

    [SerializeField]
    private float translationSpeedOnPowerUp;

    [SerializeField]
    private float translationSpeedPowerUpDuration;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject tripleBulletPrefab;

    [SerializeField]
    private float tripleShotPowerUpDuration;

    [SerializeField]
    private int bulletVerticalDirection;

    [SerializeField]
    private float bulletTranslationSpeed;

    [SerializeField]
    private GameObject shield;

    private GameObject bulletTypePrefab;

    [SerializeField]
    private float shieldPowerUpDuration;

    private void Start()
    {
        shield.SetActive(false);
        bulletTypePrefab = bulletPrefab;
    }

    void Update()
    {
        if (lives == 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Space)) Shoot();

        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Meteor" || collision.tag == "Enemy" || collision.tag == "EnemyBullet")
        {
            if (shield.activeSelf) shield.SetActive(false);
            else lives--;

            // Could have been done in the EnemyBulletController but I don't have one
            if (collision.tag == "EnemyBullet") Destroy(collision.gameObject);
        }

        if (collision.tag == "Speed")
        {
            Destroy(collision.gameObject);
            StartCoroutine(DeactivateSpeedPowerUp(translationSpeed));
            translationSpeed = translationSpeedOnPowerUp;
        }

        if (collision.tag == "TripleShot")
        {
            Destroy(collision.gameObject);
            StartCoroutine(DeactiveTripleShotPowerUp());
            bulletTypePrefab = tripleBulletPrefab;
        }

        if (collision.tag == "Shield")
        {
            Destroy(collision.gameObject);
            StartCoroutine(DeactivateShieldPowerUp());
            shield.SetActive(true);
        }
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 translation = new Vector3(inputX, inputY, 0) * translationSpeed * Time.deltaTime;
        Vector3 nextPosition = transform.position + translation;
        float clampedX = Mathf.Clamp(nextPosition.x, -8f, 8f);
        float clampedY = Mathf.Clamp(nextPosition.y, -4f, 4f);
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, 0);
        transform.position = clampedPosition;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletTypePrefab);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        // Config bullet vertical direction
        bulletController.verticalDirection = bulletVerticalDirection;

        // Config bullet velocity
        bulletController.translationSpeed = bulletTranslationSpeed;

        // Config bullet position
        Vector3 playerPosition = transform.position;
        Vector3 position = new Vector3(playerPosition.x, playerPosition.y + 1, 0);
        bullet.transform.position = position;

        Destroy(bullet, 3);
    }

    IEnumerator DeactivateSpeedPowerUp(float originalTranslationSpeed)
    {
        yield return new WaitForSeconds(translationSpeedPowerUpDuration);
        translationSpeed = originalTranslationSpeed;
    }

    IEnumerator DeactiveTripleShotPowerUp()
    {
        yield return new WaitForSeconds(tripleShotPowerUpDuration);
        bulletTypePrefab = bulletPrefab;
    }

    IEnumerator DeactivateShieldPowerUp()
    {
        yield return new WaitForSeconds(shieldPowerUpDuration);
        shield.SetActive(false);
    }
}
