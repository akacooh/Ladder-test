using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputController inputController;

    public event Action PlayerJumpedForward;
    public event Action PlayerDied;

    private float movingTime = 0.5f;
    private bool moving = false;

    private int currentPosition = 2;

    void Start()
    {
        Subscribe();
    }

    private void OnSwipeLeft() {
        SideJump(false);
        currentPosition--;
        if (currentPosition < 0) Destroy(gameObject);
    }

    private void OnSwipeRight() {
        SideJump(true);
        currentPosition++;
        if (currentPosition > 5) Destroy(gameObject);
    }

    private void OnTap() {
        ForwardJump();
    }

    private void OnDestroy() {
        if (inputController != null) {
            UnSubscribe();
        }
        PlayerDied?.Invoke();
    }

    private void Subscribe() {
        inputController.Tap += OnTap;
        inputController.SwipeLeft += OnSwipeLeft;
        inputController.SwipeRight += OnSwipeRight;
    }

    private void UnSubscribe() {
        inputController.Tap -= OnTap;
        inputController.SwipeLeft -= OnSwipeLeft;
        inputController.SwipeRight -= OnSwipeRight;
    }

    private void SideJump(bool right) {
        if (moving) return;

        moving = true;
        if (right) {
            StartCoroutine(MoveParabolic(new Vector3(0, 0, 1), 1));
        }
        else {
            StartCoroutine(MoveParabolic(new Vector3(0, 0, -1), 1));
        }
    }

    private void ForwardJump() {
        if (moving) return;

        moving = true;
        StartCoroutine(MoveParabolic(new Vector3(-1, 1, 0), 1));
        PlayerJumpedForward?.Invoke();
    }

    private IEnumerator MoveParabolic(Vector3 deltaPosition, float height)
    {
        float elapsedTime = 0;
        Vector3 desiredPosition = transform.position + deltaPosition;
        Vector3 startPosition = transform.position;
        while (elapsedTime < movingTime)
        {
            float t = elapsedTime / movingTime;
            float y = 4 * height * (t - t * t);
            transform.position = Vector3.Lerp(startPosition, desiredPosition, t) + y * Vector3.up;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = desiredPosition;

        moving = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}
