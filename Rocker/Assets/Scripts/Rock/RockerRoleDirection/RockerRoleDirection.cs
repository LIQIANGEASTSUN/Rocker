﻿using UnityEngine;

public class RockerRoleDirection : IRock
{
    private Transform _roleTr;
    private float _speed = 1.5f;
    private Vector2 _lastPos = Vector3.zero;
    private ICameraFollow _icameraFollow;
    public RockerRoleDirection(ICameraFollow cameraFollow)
    {
        _roleTr = GameObject.Find("Role").transform;
        _icameraFollow = cameraFollow;
        _icameraFollow.SetInfo(_roleTr, Vector3.zero);
    }

    public void Begin(Vector2 pos)
    {
        _lastPos = pos;
    }

    public void Move(Vector2 pos, float percentage)
    {
        Vector2 offset = (pos - _lastPos);
        if (offset.sqrMagnitude <= 0)
        {
            return;
        }
        _lastPos = pos;

        // 左右滑动控制人的转向
        Quaternion rotation = Quaternion.AngleAxis(offset.x, Vector3.up);
        _roleTr.rotation = rotation * _roleTr.rotation;
        _icameraFollow.Move();
    }

    public void End(Vector2 pos)
    {

    }
}
