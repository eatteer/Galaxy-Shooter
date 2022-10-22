using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemyPrefab;
        public int enemiesCount;
        public int spawnRate;
    }

    public List<Wave> waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5;
    public float waveCountdown;

    private float searchCountdown = 1;

    private SpawnState spawnState = SpawnState.Counting;

    void Start()
    {
        // Set {waveCountdown} for spawnig waves
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (spawnState == SpawnState.Waiting)
        {
            // Check if enemies are still alive
            if (!IsEnemyAlive())
            {
                // Begin a new wave
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        // Check if {waveCountdown} has reached 0 to spawn the next wave
        if (waveCountdown <= 0)
        {
            // Another wave is not spawning
            if (spawnState != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            // As {waveCountdown} has not reached 0, then decrement its value
            // until it is 0
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");
        spawnState = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Count - 1)
        {
            nextWave = 0;
            Debug.Log("All waves completed - Restarting Waves");
            return;
        }

        nextWave++;
    }

    bool IsEnemyAlive()
    {
        // Search for enemy every second and not every frame
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0)
        {
            searchCountdown = 1;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        spawnState = SpawnState.Spawning;

        for (int i = 0; i < _wave.enemiesCount; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1 / _wave.spawnRate);
        }

        spawnState = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(GameObject _enemyPrefab)
    {
        Debug.Log("Spawning Enemy: " + _enemyPrefab.name);
        Instantiate(_enemyPrefab, transform);
    }
}
