using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    private float animationSpeed;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Vector2 textureOffset = new Vector2(0, animationSpeed) * Time.deltaTime;
        _meshRenderer.material.mainTextureOffset += textureOffset;
    }
}
