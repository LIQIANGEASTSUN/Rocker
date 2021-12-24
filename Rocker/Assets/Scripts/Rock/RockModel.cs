using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 旋转模型（UI中展示的模型）
/// </summary>
public class RockModel : IRock
{
    private IInputModelRocker inputModelRocker;
    public RockModel()
    {
        
    }
    public void Begin(Vector2 pos)
    {
        if (null != inputModelRocker)
        {
            inputModelRocker.ModelBegineMove(Vector3.zero);
        }
    }

    public void Move(Vector2 pos, float percentage)
    {
        Vector3 dir = pos;
        if (dir.magnitude <= 0)
        {
            return;
        }
        if (null != inputModelRocker)
        {
            inputModelRocker.ModelMoving(dir);
        }
    }

    public void End(Vector2 pos)
    {
        if (null != inputModelRocker)
        {
            inputModelRocker.ModelEndMove(Vector3.zero);
        }
    }
}
