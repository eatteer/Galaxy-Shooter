using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject meteorPrefab;

    [SerializeField]
    private int horizontalDirection;

    [SerializeField]
    private float velocity;

    [SerializeField]
    private float interval;
    void Start()
    {
        InvokeRepeating(nameof(SpawnMeteor), interval, interval);
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
        meteorController.horizontalDirection = horizontalDirection;
        meteorController.velocity = velocity;
        float meteorXPosition = spawnerXPosition + horizontalDirection;
        meteor.transform.position = new Vector3(meteorXPosition, meteorYPosition, 0);
    }
}
