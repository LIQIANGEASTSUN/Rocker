using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆操作人移动,只向人的前方移动
/// </summary>
public class RockerMoveForwardReceive : IRocker
{
    public void Begin(Vector2 pos)
    {

    }

    public void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        Vector3 dir = RoleController.GetInstance().RoleForward;
        RoleController.GetInstance().Move(dir);
    }

    public void End(Vector2 pos)
    {

    }
}
