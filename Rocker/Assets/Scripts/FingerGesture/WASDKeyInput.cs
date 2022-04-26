using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WASDKeyInput
{
    private Vector2 _center;

    private List<KeyValuePair<KeyCode, Vector2>> _keyList = new List<KeyValuePair<KeyCode, Vector2>>() {
        new KeyValuePair<KeyCode, Vector2>(KeyCode.W, Vector2.up),
        new KeyValuePair<KeyCode, Vector2>(KeyCode.S, Vector2.down),
        new KeyValuePair<KeyCode, Vector2>(KeyCode.A, Vector2.left),
        new KeyValuePair<KeyCode, Vector2>(KeyCode.D, Vector2.right),
    };

    private const int _fingerId = 10000;
    private KeyInputStateMachine _keyInputStateMachine;
    private int _oldKey;
    private Vector2 _oldPosition;

    public WASDKeyInput(Vector2 center)
    {
        _center = center;
        _keyInputStateMachine = new KeyInputStateMachine(_fingerId, _center);
        _keyInputStateMachine.ChangeState(KeyInputState.None, 0, Vector2.zero);
    }

    public void Update()
    {
        Touch touch = new Touch();
        touch.fingerId = _fingerId;

        Vector2 dir = Vector2.zero;
        int key = 0;
        for (int i = 0; i < _keyList.Count; ++i)
        {
            KeyValuePair<KeyCode, Vector2> kv = _keyList[i];
            if (Input.GetKeyDown(kv.Key) || Input.GetKey(kv.Key))
            {
                key |= (1 << i);
                dir += kv.Value;
            }
        }

        if (dir.sqrMagnitude <= 0)
        {
            key = 0;
        }

        _keyInputStateMachine.Execute(key, dir);
    }

}

public enum KeyInputState
{
    None,
    Move,
}

public class KeyInputStateMachine
{
    private Dictionary<KeyInputState, KeyInputAB> _keyInputDic = new Dictionary<KeyInputState, KeyInputAB>();
    private KeyInputAB _currentState;
    public KeyInputStateMachine(int fingerId, Vector2 center)
    {
        _keyInputDic[KeyInputState.None] = new KeyInputNode();
        _keyInputDic[KeyInputState.Move] = new KeyInputMove();
        foreach(var kv in _keyInputDic)
        {
            kv.Value.SetFingerId(fingerId);
            kv.Value.SetCenter(center);
            kv.Value.SetChangeEvent(ChangeState);
        }
    }

    public void Execute(int key, Vector2 dir)
    {
        _currentState.Excute(key, dir);
    }

    public void ChangeState(KeyInputState state, int key, Vector2 dir)
    {
        if (null != _currentState)
        {
            _currentState.Exit(key, dir);
        }
        _currentState = _keyInputDic[state];
        _currentState.OnEntry(key, dir);
    }

}

public abstract class KeyInputAB
{
    protected int _fingerId;
    protected Vector2 _center;
    protected Action<KeyInputState, int, Vector2> _changeStateEvent;

    public void SetFingerId(int fingerId)
    {
        _fingerId = fingerId;
    }

    public void SetCenter(Vector2 center)
    {
        _center = center;
    }

    public void SetChangeEvent(Action<KeyInputState, int, Vector2> changeEvent)
    {
        _changeStateEvent = changeEvent;
    }

    public abstract void OnEntry(int key, Vector2 dir);

    public abstract void Excute(int key, Vector2 dir);

    public abstract void Exit(int key, Vector2 dir);

    protected Touch CreateTouch()
    {
        Touch touch = new Touch();
        touch.fingerId = _fingerId;
        return touch;
    }
}

public class KeyInputNode : KeyInputAB
{


    public override void OnEntry(int key, Vector2 dir)
    {

    }

    public override void Excute(int key, Vector2 dir)
    {
        if (key == 0)
        {
            return;
        }

        Touch touch = CreateTouch();
        touch.phase = TouchPhase.Moved;
        touch.position = _center;
        touch.deltaPosition = Vector2.zero;
        FingerGestureSystem.GetInstance().AddCustomTouch(touch);

        _changeStateEvent(KeyInputState.Move, key, dir);
    }

    public override void Exit(int key, Vector2 dir)
    {

    }

}

public class KeyInputMove : KeyInputAB
{
    private int _lastKey = 0;
    private Vector2 _lastPoint;
    public override void OnEntry(int key, Vector2 dir)
    {
        Touch touch = CreateTouch();
        touch.phase = TouchPhase.Moved;
        touch.position = _center + dir;
        touch.deltaPosition = dir;
        FingerGestureSystem.GetInstance().AddCustomTouch(touch);

        _lastKey = key;
        _lastPoint = touch.position;
    }

    public override void Excute(int key, Vector2 dir)
    {
        if (key == 0)
        {
            _changeStateEvent(KeyInputState.None, key, dir);
            return;
        }

        if (_lastKey != key)
        {
            Touch touch = CreateTouch();
            touch.phase = TouchPhase.Moved;
            touch.position = _center + dir;
            touch.deltaPosition = _lastPoint - touch.position;
            FingerGestureSystem.GetInstance().AddCustomTouch(touch);

            _lastKey = key;
            _lastPoint = touch.position;
        }
    }

    public override void Exit(int key, Vector2 dir)
    {
        Touch touch = CreateTouch();
        touch.phase = TouchPhase.Ended;
        touch.position = _center;
        touch.deltaPosition = Vector2.zero;
        FingerGestureSystem.GetInstance().AddCustomTouch(touch);

        _lastKey = 0;
        _lastPoint = _center;
    }

}