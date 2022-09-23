using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Value set by PlayerController
    [HideInInspector]
    public float velocity;

    // Value set by PlayerController
    [HideInInspector]
    public int verticalDirection;

    void Update()
    {
        Vector3 movement = new Vector3(0, verticalDirection, 0) * velocity * Time.deltaTime;
        this.transform.Translate(movement);
    }
}
