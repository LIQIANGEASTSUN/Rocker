using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerUIModelRotate : IRock
{
    private Transform _roleTr;
    private float _speed = 1.5f;
    private Vector2 _lastPos = Vector3.zero;
    public RockerUIModelRotate()
    {
        _roleTr = GameObject.Find("RoleParent").transform;
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

        //// 左右滑动控制人的转向
        Quaternion rotation = Quaternion.AngleAxis(offset.x, Vector3.up);
        _roleTr.rotation = rotation * _roleTr.rotation;
    }

    public void End(Vector2 pos)
    {

    }
}
