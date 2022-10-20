﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FingerTouchDown(int fingerId, Vector2 position);
public delegate void FingerTouchUp(int fingerId, Vector2 position);
public delegate void FingerTouchBeginDrag(int fingerId, Vector2 position);
public delegate void FingerTouchDrag(int fingerId, Vector2 position, Vector2 deltaPosition);
public delegate void FingerTouchDragEnd(int fingerId, Vector2 pisition);

public class FingerGestureSystem : SingletonObject<FingerGestureSystem>
{
    private List<FingerGesture> fingerGesturesList = new List<FingerGesture>();

    public FingerTouchDown fingerTouchDown;
    public FingerTouchUp fingerTouchUp;
    public FingerTouchBeginDrag fingerTouchBeginDrag;
    public FingerTouchDrag fingerTouchDrag;
    public FingerTouchDragEnd fingerTouchDragEnd;

    public FingerGestureSystem()
    {

    }

    public void Update()
    {
        ReceiveInput();
        Execute();
    }

    private int[] mouseIds = new int[] { 0, 1, 2 };
    private void ReceiveInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        PCReceiveInput();
#endif

#if (!UNITY_EDITOR) && (UNITY_IOS || UNITY_ANDROID)
        MobileReceiveInput();
#endif
    }

    private void PCReceiveInput()
    {
        for (int i = 0; i < mouseIds.Length; ++i)
        {
            int fingerId = mouseIds[i];

            bool input = false;
            Vector2 deltaPosition = Vector2.zero;
            TouchPhase touchPhase = TouchPhase.Began;

            if (Input.GetMouseButtonDown(fingerId))
            {
                input = true;
                deltaPosition = Vector2.zero;
                touchPhase = TouchPhase.Began;
            }
            else if (Input.GetMouseButton(fingerId))
            {
                FingerGesture fingerGesture = GetFingerGestureIfNullCreate(fingerId);
                input = true;
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                deltaPosition = fingerGesture.LastPosition() - mousePosition;
                if (deltaPosition.sqrMagnitude > 0)
                {
                    touchPhase = TouchPhase.Moved;
                }
                else
                {
                    touchPhase = TouchPhase.Stationary;
                }
            }
            else if (Input.GetMouseButtonUp(fingerId))
            {
                input = true;
                deltaPosition = Vector2.zero;
                touchPhase = TouchPhase.Ended;
            }

            if (input && touchPhase != TouchPhase.Stationary)
            {
                Touch touch = new Touch();
                touch.fingerId = fingerId;
                touch.position = Input.mousePosition;
                touch.deltaPosition = deltaPosition;
                touch.phase = touchPhase;
                FingerGesture fingerGesture = GetFingerGestureIfNullCreate(fingerId);
                fingerGesture.AddTouch(touch);
            }
        }
    }

    private void MobileReceiveInput()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Touch touch = Input.GetTouch(i);
            FingerGesture fingerGesture = GetFingerGestureIfNullCreate(touch.fingerId);
            fingerGesture.AddTouch(touch);
        }
    }

    public void AddCustomTouch(Touch touch)
    {
        FingerGesture fingerGesture = GetFingerGestureIfNullCreate(touch.fingerId);
        fingerGesture.AddTouch(touch);
    }

    private void Execute()
    {
        foreach (var gesture in fingerGesturesList)
        {
            gesture.Execute();
        }
    }

    private FingerGesture GetFingerGestureIfNullCreate(int fingerId)
    {
        FingerGesture fingerGesture = GetFingerGesture(fingerId);
        if (null == fingerGesture)
        {
            fingerGesture = new FingerGesture(fingerId);
            fingerGesturesList.Add(fingerGesture);
        }
        return fingerGesture;
    }

    private FingerGesture GetFingerGesture(int fingerId)
    {
        FingerGesture fingerGesture = null;
        for (int i = 0; i < fingerGesturesList.Count; ++i)
        {
            FingerGesture temp = fingerGesturesList[i];
            if (temp.FingerId == fingerId)
            {
                fingerGesture = temp;
            }
        }
        return fingerGesture;
    }

}

public class FingerGesture
{
    private int _fingerId;
    private Touch _lastTouch;
    private Queue<Touch> _queueTouch = new Queue<Touch>();

    private bool _beginDrag = false;

    public FingerGesture(int fingerId)
    {
        _fingerId = fingerId;
    }

    public void AddTouch(Touch touch)
    {
        if (touch.fingerId == _fingerId)
        {
            _queueTouch.Enqueue(touch);
        }
        else
        {
            Debug.LogError("Different touch:" + touch.fingerId);
        }
    }

    public Vector2 LastPosition()
    {
        return _lastTouch.position;
    }

    public void Execute()
    {
        if (_queueTouch.Count <= 0)
        {
            return;
        }

        Touch touch = _queueTouch.Dequeue();
        if (touch.phase == TouchPhase.Began)
        {
            Down(touch);
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Move(touch);
        }
        else if (touch.phase == TouchPhase.Stationary)
        {

        }
        else if (touch.phase == TouchPhase.Ended)
        {
            Up(touch);
        }

        _lastTouch = touch;
    }

    private void Down(Touch touch)
    {
        _beginDrag = false;
        //Debug.LogError("Down");
        if (null != FingerGestureSystem.GetInstance().fingerTouchDown)
        {
            FingerGestureSystem.GetInstance().fingerTouchDown(_fingerId, touch.position);
        }
    }

    private void Move(Touch touch)
    {
        if (!_beginDrag)
        {
            _beginDrag = true;
            //Debug.LogError("BeginDrag");
            if (null != FingerGestureSystem.GetInstance().fingerTouchBeginDrag)
            {
                FingerGestureSystem.GetInstance().fingerTouchBeginDrag(_fingerId, touch.position);
            }
        }
        else
        {
            //Debug.LogError("Drag:" + touch.position + "    " + touch.deltaPosition);
            if (null != FingerGestureSystem.GetInstance().fingerTouchDrag)
            {
                FingerGestureSystem.GetInstance().fingerTouchDrag(_fingerId, touch.position, touch.deltaPosition);
            }
        }
    }

    private void Up(Touch touch)
    {
        //Debug.LogError("Up");
        if (null != FingerGestureSystem.GetInstance().fingerTouchUp)
        {
            FingerGestureSystem.GetInstance().fingerTouchUp(_fingerId, touch.position);
        }
        if (_beginDrag)
        {
            _beginDrag = false;
            //Debug.LogError("EndDrag");
            if (null != FingerGestureSystem.GetInstance().fingerTouchDragEnd)
            {
                FingerGestureSystem.GetInstance().fingerTouchDragEnd(_fingerId, touch.position);
            }
        }
    }

    public int FingerId
    {
        get { return _fingerId; }
    }
}
