using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆拖拽操作人转向
/// 向原始位置左侧拖拽人向逆时针旋转
/// 向原始位置右侧拖拽人向顺时针旋转
/// </summary>
public class RockerDragRotateController : IRock
{
    private Vector2 _lastPos = Vector3.zero;
    public RockerDragRotateController()
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
        float rotateSpeed = RoleController.GetInstance().RotateSpeed;
        Quaternion rotation = Quaternion.AngleAxis(offset.x * rotateSpeed, Vector3.up);
        Quaternion roleRotate = RoleController.GetInstance().RoleRotate;
        RoleController.GetInstance().Rotate(rotation * roleRotate);
    }

    public void End(Vector2 pos)
    {

    }
}
