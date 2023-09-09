using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public event Action SwipeLeft;
    public event Action SwipeRight;
    public event Action Tap;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private float precision = 10;
    private bool touchedUI; //Disable jump when touching UI

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
                touchedUI = true;
                return;
            }
            startTouchPosition = Input.GetTouch(0).position;
            touchedUI = false;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !touchedUI) {
            endTouchPosition = Input.GetTouch(0).position;
            if (Mathf.Abs(endTouchPosition.x - startTouchPosition.x) < precision) Tap?.Invoke();
            else if (endTouchPosition.x > startTouchPosition.x) SwipeRight?.Invoke();
            else if (endTouchPosition.x < startTouchPosition.x) SwipeLeft?.Invoke();
        }
    }
}
