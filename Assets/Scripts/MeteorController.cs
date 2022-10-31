using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    // Value set by MeteorSpawnerController
    [HideInInspector]
    public int horizontalDirection;

    // Value set by MeteorSpawnerController
    [HideInInspector]
    public float translationSpeeed;

    GameObject explosionAudio;

    private void Start()
    {
        explosionAudio = transform.Find("ExplosionAudio").gameObject;
    }

    void Update()
    {
        Vector3 translation = new Vector3(horizontalDirection, 0, 0) * translationSpeeed * Time.deltaTime;
        this.transform.Translate(translation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag == "Player")
        {
            translationSpeeed = 0;
            explosionAudio.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 2);
        }
    }
}
