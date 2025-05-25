using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;

    [SerializeField]
    private GameObject[] powerUps;

    private float _xMin = -8f;
    private float _xMax = 8f;
    private float _ySpawn = 7f;

    private bool _stopSpawning = false;

    public void StopSpawning()
    {
        _stopSpawning = true;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    

    private IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            float randomX = Random.Range(_xMin, _xMax);
            Vector3 spawnPos = new Vector3(randomX, _ySpawn, 0);
            Instantiate(enemyShipPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
        }
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(5f, 9f));
            int randomIndex = Random.Range(0, powerUps.Length);
            float randomX = Random.Range(_xMin, _xMax);
            Vector3 spawnPos = new Vector3(randomX, _ySpawn, 0);
            Instantiate(powerUps[randomIndex], spawnPos, Quaternion.identity);
        }
    }

    public void StartSpawning()
    {
        _stopSpawning = false;
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
}
