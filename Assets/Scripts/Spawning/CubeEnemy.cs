using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : MonoBehaviour, IEnemy, ISpawnable
{
    private float speed = 2;
    private Vector3 spawnedPosition;

    public void Move() {
        transform.position += speed * Time.deltaTime * new Vector3(1, -1, 0);
    }

    void Update() {
        Move();
        CheckForBoundaries();
    }
    
    public void SetPosition(Vector3 position) {
        transform.position = position;
        spawnedPosition = position;
    }

    public void CheckForBoundaries() {
        if (Mathf.Abs(spawnedPosition.y - transform.position.y ) > 20) Destroy(gameObject);
    }
}
