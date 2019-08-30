using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public WaveConfig[] waves;

    public Transform tower;
    public Transform enemySpawnPointsParent;

    public float waveCooldownTime = 5f;
    public bool repeatWavesAfterDone;

    private WaitForSeconds enemySpawnDelay;
    private WaitForSeconds waveCooldown;

    private List<Enemy> liveEnemies = new List<Enemy>();
    private Queue<WaveConfig> waveQueue = new Queue<WaveConfig>();

    void Start()
    {
        waveCooldown = new WaitForSeconds(waveCooldownTime);

        foreach (WaveConfig wave in waves)
            waveQueue.Enqueue(wave);

        StartSpawningEnemies();
    }

    private void StartSpawningEnemies()
    {
        StartCoroutine(EnemySpawnSequence());
    }

    private IEnumerator EnemySpawnSequence()
    {
        WaveConfig currentWave = waveQueue.Dequeue();
        int enemiesToSpawn = currentWave.GetNumberOfEnemies();
        Enemy enemyPrefab = currentWave.GetEnemyPrefab();
        float timeBetweenSpawns = currentWave.GetTimeBetweenSpawns();

        enemySpawnDelay = new WaitForSeconds(timeBetweenSpawns);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // TODO - add condition here to stop when the control tower gets destroyed
            // if tower is not dead spawn another enemy

            Enemy enemy = Instantiate(enemyPrefab);
            enemy.transform.position = enemySpawnPointsParent.GetChild(UnityEngine.Random.Range(0, enemySpawnPointsParent.childCount)).position;

            enemy.Init(tower);

            liveEnemies.Add(enemy);

            yield return enemySpawnDelay;
        }

        yield return StartCoroutine(WaveCDSequence());

        if (repeatWavesAfterDone)
            waveQueue.Enqueue(currentWave);

        StartSpawningEnemies();
    }

    private IEnumerator WaveCDSequence()
    {
        yield return waveCooldown;
        yield break;
    }
}
