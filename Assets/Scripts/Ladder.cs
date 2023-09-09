using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject step;
    [SerializeField] private int numberOfSteps;

    private Queue<GameObject> steps;

    public Vector3 spawnPosition { get; private set; }
    
    void Start()
    {
        spawnPosition = transform.position;
        player.PlayerJumpedForward += OnTap;
        InitialiseQueue();
    }

    private void InitialiseQueue() {
        steps = new Queue<GameObject>();
        for (int i = 0; i < numberOfSteps; i++) {
            var newStep = Instantiate(step, gameObject.transform, false);
            newStep.transform.localPosition = spawnPosition;
            IncreaseSpawnPosition();
            steps.Enqueue(newStep);
        }
    }

    private void OnTap() {
        CycleSteps();
    }

    private void CycleSteps() {
        var lowestStep = steps.Dequeue();
        lowestStep.transform.position = spawnPosition;
        IncreaseSpawnPosition();
        steps.Enqueue(lowestStep);
    }

    private void IncreaseSpawnPosition() {
        spawnPosition = new Vector3(spawnPosition.x - 1, spawnPosition.y + 1, 0);
    }
}
