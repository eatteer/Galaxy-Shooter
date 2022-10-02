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

    void Update()
    {
        Vector3 translation = new Vector3(horizontalDirection, 0, 0) * translationSpeeed * Time.deltaTime;
        this.transform.Translate(translation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
