using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject kamikazeEnemyPrefab;
    public GameObject shootingEnemyPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 5f;
    public float waveDelay = 1f;
    public int initialWaveSize = 5;
    public float kamikazeChance = 0.75f;
    public float shootingChance = 0.25f;
    public float kamikazeChanceIncrement = -0.025f;
    public float shootingChanceIncrement = 0.025f;
    public float minKamikazeChance = 0.5f;
    public float maxShootingChance = 0.5f;

    private int currentWaveSize;
    private float currentKamikazeChance;
    private float currentShootingChance;
    private int enemiesRemaining;

    private void Start()
    {
        currentWaveSize = initialWaveSize;
        currentKamikazeChance = kamikazeChance;
        currentShootingChance = shootingChance;
        enemiesRemaining = 0;

        Invoke("SpawnWave", spawnDelay);
    }

    private void SpawnWave()
    {
        if (enemiesRemaining <= 0)
        {
            for (int i = 0; i < currentWaveSize; i++)
            {
                GameObject enemyPrefab = GetRandomEnemyPrefab();
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemyPrefab, spawnPosition, spawnRotation);
                enemiesRemaining++;
            }

            enemiesRemaining = currentWaveSize;
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        float randomValue = Random.value;
        if (randomValue < currentKamikazeChance)
        {
            return kamikazeEnemyPrefab;
        }
        else
        {
            return shootingEnemyPrefab;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float xPosition = Random.Range(-5f, 5f);
        float yPosition = spawnPoint.position.y;
        float zPosition = spawnPoint.position.z;
        return new Vector3(xPosition, yPosition, zPosition);
    }

    public void EnemyDestroyed()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            Invoke("SpawnWave", waveDelay);
            UpdateWaveParameters();
        }
    }

    private void UpdateWaveParameters()
    {
        currentWaveSize += 2;
        currentKamikazeChance += kamikazeChanceIncrement;
        currentShootingChance += shootingChanceIncrement;

        currentKamikazeChance = Mathf.Clamp(currentKamikazeChance, minKamikazeChance, 1f);
        currentShootingChance = Mathf.Clamp(currentShootingChance, 0f, maxShootingChance);
    }
}
