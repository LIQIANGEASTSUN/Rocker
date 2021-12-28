using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerUIModelRotateReceive : IRock
{
    private Vector2 _lastPos = Vector3.zero;
    public RockerUIModelRotateReceive()
    {
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
        float rotateSpeed = UIRoleController.GetInstance().RotateSpeed;
        Quaternion rotation = Quaternion.AngleAxis(offset.x * rotateSpeed, Vector3.up);
        Quaternion roleRotate = UIRoleController.GetInstance().RoleRotate;
        UIRoleController.GetInstance().Rotate(rotation * roleRotate);
    }

    public void End(Vector2 pos)
    {

    }
}
