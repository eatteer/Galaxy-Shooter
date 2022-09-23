using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [HideInInspector]
    public int horizontalDirection;

    [HideInInspector]
    public float velocity;

    void Update()
    {
        Vector3 movement = new Vector3(horizontalDirection, 0, 0) * velocity * Time.deltaTime;
        this.transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MeteorSpawner")
        {
            Destroy(this.gameObject);
        }
    }
}
