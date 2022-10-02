using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    void Update()
    {
        Vector3 rotation = new Vector3(0, rotationSpeed, 0);
        this.transform.Rotate(rotation);
    }
}
