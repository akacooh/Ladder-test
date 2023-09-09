using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : Factory
{
    [SerializeField] CubeEnemy enemyPrefab;

    public override ISpawnable Create() {
        var instance = Instantiate(enemyPrefab.gameObject);
        CubeEnemy cubeEnemy = instance.GetComponent<CubeEnemy>();
        return cubeEnemy;
    }
}
