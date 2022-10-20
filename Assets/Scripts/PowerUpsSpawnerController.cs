using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsSpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject shieldPrefab;

    [SerializeField]
    private GameObject speedPrefab;
    
    [SerializeField]
    private GameObject tripleShootPrefab;

    private List<GameObject> powerUpsPrefabs = new List<GameObject>();
    private float intervalGeneration = 3;
    private float timeElapsed;

    void Start()
    {
        powerUpsPrefabs.Add(shieldPrefab);
        powerUpsPrefabs.Add(speedPrefab);
        powerUpsPrefabs.Add(tripleShootPrefab);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= intervalGeneration)
        {
            float edge = transform.localScale.x / 2;
            int randomIndex = Random.Range(0, 3);
            float spawnXPosition = Mathf.Clamp(Random.Range(-edge, edge + 1), -edge, edge);
            GameObject randomPrefab = powerUpsPrefabs[randomIndex];
            GameObject randomPowerUp = Instantiate(randomPrefab);
            randomPrefab.transform.position = new Vector3(spawnXPosition, 6, 0);
            timeElapsed = 0;
            Destroy(randomPowerUp, 4);
        }

    }
}
