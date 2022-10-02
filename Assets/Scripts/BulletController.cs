using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Value set by PlayerController
    [HideInInspector]
    public int verticalDirection;

    // Value set by PlayerController
    [HideInInspector]
    public float translationSpeed;

    void Update()
    {
        Vector3 translation = new Vector3(0, verticalDirection, 0) * translationSpeed * Time.deltaTime;
        this.transform.Translate(translation);
    }
}
