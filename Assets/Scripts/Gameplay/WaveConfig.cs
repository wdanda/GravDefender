using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wav Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private GameObject pathPrefab = null;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 7;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitBeforeNextWave = 2f;

    public GameObject GetEnemyPrefab() => enemyPrefab;

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() => timeBetweenSpawns;

    public float GetSpawnRandomFactor() => spawnRandomFactor;

    public int GetNumberOfEnemies() => numberOfEnemies;

    public float GetMoveSpeed() => moveSpeed;

    public float GetWaitBeforeNextWave() => waitBeforeNextWave;

}