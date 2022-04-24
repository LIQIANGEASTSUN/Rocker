using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆拖拽操作人转向
/// 向原始位置左侧拖拽人向逆时针旋转
/// 向原始位置右侧拖拽人向顺时针旋转
/// </summary>
public class CameraRotateReceive : IRocker
{
    public CameraRotateReceive()
    {
    }

    public void Begin(Vector2 pos)
    {
    }

    public void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        if (deltaPoint.sqrMagnitude <= 0)
        {
            return;
        }

        //// 左右滑动控制人的转向
        float rotateSpeed = RoleController.GetInstance().RotateSpeed;
        Quaternion rotation = Quaternion.AngleAxis(deltaPoint.x * rotateSpeed, Vector3.up);
        Quaternion roleRotate = RoleController.GetInstance().RoleRotate;
        RoleController.GetInstance().Rotate(rotation * roleRotate);
    }

    public void End(Vector2 pos)
    {

    }

}
