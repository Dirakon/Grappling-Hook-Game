using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
    void Start()
    {
        if (GetComponent<Tutorial>() != null)
            ForbidInput();
    }
    public void ForbidInput()
    {
        inputForbidden = true;
    }
    public void UnforbidInput()
    {
        inputForbidden = false;
    }
    public bool inputForbidden;
    public float distanceDelta = 0;
    public Vector2 mainTouchPosition;
    private float currentDistance = 0;
    public UnityAction onTouchesStop;
    public bool[] fingerPresent = { false, false };
    void LastTouchStop()
    {
    }
    void FirstTouchStart(Touch touch1)
    {
        UpdateFirstTouch(touch1);
    }
    void UpdateFirstTouch(Touch touch1)
    {
        mainTouchPosition = touch1.position;
    }
    void InitializeDeltaCounter(Touch touch1, Touch touch2)
    {
        currentDistance = (touch1.position - touch2.position).magnitude;
    }
    void StopDeltaCounter()
    {
        distanceDelta = 0;
        currentDistance = 0;
    }
    void UpdateDelta(Touch touch1, Touch touch2)
    {
        float newDistance = (touch1.position - touch2.position).magnitude;
        distanceDelta = newDistance - currentDistance;
        currentDistance = newDistance;
    }
    void Update()
    {
        if (inputForbidden)
        {
            fingerPresent[0] = fingerPresent[1] = false;
            return;
        }
        if (Input.touchCount == 1)
        {
            fingerPresent[0] = true;
            fingerPresent[1] = false;
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Canceled:
                    goto case TouchPhase.Ended;
                case TouchPhase.Ended:
                    LastTouchStop();
                    break;
                case TouchPhase.Began:
                    FirstTouchStart(touch);
                    break;
                case TouchPhase.Moved:
                    UpdateFirstTouch(touch);
                    break;
            }
        }
        else if (Input.touchCount > 1)
        {
            fingerPresent[0] = fingerPresent[1] = true;
            Touch touch1 = Input.GetTouch(0), touch2 = Input.GetTouch(1);
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                InitializeDeltaCounter(touch1, touch2);
            }
            if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended ||
            touch1.phase == TouchPhase.Canceled || touch2.phase == TouchPhase.Canceled)
            {
                StopDeltaCounter();
            }
            else
            {
                UpdateDelta(touch1, touch2);
            }
        }
        else
        {
            fingerPresent[0] = fingerPresent[1] = false;
        }
    }
}
