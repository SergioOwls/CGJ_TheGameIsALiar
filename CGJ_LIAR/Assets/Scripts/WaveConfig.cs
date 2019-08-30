using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Enemy wave config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private int numberOfEnemies = 10;

    public Enemy GetEnemyPrefab() { return enemyPrefab; }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }
}
