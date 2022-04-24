using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerUIModelRotateReceive : IRocker
{
    public RockerUIModelRotateReceive()
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
        float rotateSpeed = UIRoleController.GetInstance().RotateSpeed;
        Quaternion rotation = Quaternion.AngleAxis(deltaPoint.x * rotateSpeed, Vector3.up);
        Quaternion roleRotate = UIRoleController.GetInstance().RoleRotate;
        UIRoleController.GetInstance().Rotate(rotation * roleRotate);
    }

    public void End(Vector2 pos)
    {

    }


}
