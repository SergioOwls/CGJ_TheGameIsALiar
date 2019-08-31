using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public WaveConfig[] waves;
    public string[] alienText;

    public Transform tower;
    public Transform enemySpawnPointsParent;

    public float waveCooldownTime = 5f;
    public bool repeatWavesAfterDone;

    private WaitForSeconds enemySpawnDelay;
    private WaitForSeconds waveCooldown;

    private int count;

    private Queue<WaveConfig> waveQueue = new Queue<WaveConfig>();

    void Start()
    {
        count = 0;
        waveCooldown = new WaitForSeconds(waveCooldownTime);

        foreach (WaveConfig wave in waves)
            waveQueue.Enqueue(wave);

        StartSpawningEnemies();
    }

    private void StartSpawningEnemies() { StartCoroutine(EnemySpawnSequence()); }

    private IEnumerator EnemySpawnSequence()
    {
        if(ControlTower.tower.IsDestroyed())
            yield break;

        WaveConfig currentWave = waveQueue.Dequeue();
        int enemiesToSpawn = currentWave.GetNumberOfEnemies();
        Enemy enemyPrefab = currentWave.GetEnemyPrefab();
        float timeBetweenSpawns = currentWave.GetTimeBetweenSpawns();

        enemySpawnDelay = new WaitForSeconds(timeBetweenSpawns);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (!ControlTower.tower.IsDestroyed())
                {
                    Enemy enemy = Instantiate(enemyPrefab);
                    enemy.transform.position = enemySpawnPointsParent.GetChild(UnityEngine.Random.Range(0, enemySpawnPointsParent.childCount)).position;

                    enemy.Init(tower);

                    yield return enemySpawnDelay;
                }
            }

        yield return StartCoroutine(WaveCDSequence());

        if (repeatWavesAfterDone)
            waveQueue.Enqueue(currentWave);

        StartSpawningEnemies();
    }

    private IEnumerator WaveCDSequence()
    {
        if (!Converse.say.IsBusy())
        {
            Converse.say.AlienText(alienText[count++]);
            if (count >= alienText.Length)
                count = 0;
        }

        yield return waveCooldown;
        yield break;
    }
}