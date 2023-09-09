using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Ladder ladder;
    [SerializeField] Factory enemyFactory;

    private Vector3 spawnPoint;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 3, 3);
    }

    private void Spawn() {
        spawnPoint = ladder.spawnPosition;
        Vector3 randomSpawnPoint = spawnPoint + new Vector3(0, 0, Random.Range(-2, 4)) + new Vector3(1f, 1f, -0.5f);
        ISpawnable spawn = enemyFactory.Create();
        spawn.SetPosition(randomSpawnPoint);
    }
}
