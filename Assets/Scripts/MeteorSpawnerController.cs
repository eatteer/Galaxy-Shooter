using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject meteorPrefab;

    [SerializeField]
    private int meteorHorizontalDirection;

    [SerializeField]
    private float meteorTranslationSpeed;

    [SerializeField]
    private float intervalGeneration;

    void Start()
    {
        InvokeRepeating(nameof(SpawnMeteor), intervalGeneration, intervalGeneration);
    }

    private void SpawnMeteor()
    {
        float spawnerXPosition = this.transform.position.x;
        float spawnerYPosition = this.transform.position.y;
        float spawnerYScale = this.transform.localScale.y;
        float minYPosition = spawnerYPosition - (spawnerYScale / 2);
        float maxYPosition = spawnerYPosition + (spawnerYScale / 2);
        float meteorYPosition = Random.Range(minYPosition, maxYPosition);

        // Config meteor
        GameObject meteor = Instantiate(meteorPrefab);
        MeteorController meteorController = meteor.GetComponent<MeteorController>();
        meteorController.horizontalDirection = meteorHorizontalDirection;
        meteorController.translationSpeeed = meteorTranslationSpeed;
        float meteorXPosition = spawnerXPosition;
        meteor.transform.position = new Vector3(meteorXPosition, meteorYPosition, 0);

        Destroy(meteor, 6);
    }
}
