using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 第三人称视角，摇杆同时操作角色转向和移动
/// 操作摇杆人转向，人向摇杆操作方向移动
/// </summary>
public class TPPMoveAndRotateRocker : MonoBehaviour
{
    private TPPRoleRocker _tPPRoleRocker;

    void Start()
    {
        _tPPRoleRocker = new TPPRoleRocker(transform);
        CamerFollowPositionLockDirection camerFollowPositionLockDirection = new CamerFollowPositionLockDirection();
        RoleController.GetInstance().AddCameraFollow(camerFollowPositionLockDirection);
    }

    private void OnDestroy()
    {
        _tPPRoleRocker.Release();
    }

}

public class TPPRoleRocker : RockerAB
{
    private List<IRocker> _rockerList = new List<IRocker>();
    public TPPRoleRocker(Transform transform)
    {
        // 设置开始拖拽区域为：从屏幕左下角到屏幕正中间之间的位置
        // 只要是在左侧屏幕开始拖拽，都能产生拖拽效果
        Vector2 minPickPos = new Vector2(0, 0);
        Vector2 pickSize = new Vector2(Screen.width * 0.5f, Screen.height);
        Rect pickArea = new Rect(minPickPos, pickSize);
        Init(pickArea);

        // 控制人转向
        RockerMoveRotateReceive rockerRotateReceive = new RockerMoveRotateReceive();
        _rockerList.Add(rockerRotateReceive);
        // 控制摇杆按钮上的指示方向的箭头
        RolerRockerArrowReceive rolerRockerArrowReceive = new RolerRockerArrowReceive(transform);
        _rockerList.Add(rolerRockerArrowReceive);
    }

    public override void Begin(Vector2 pos)
    {
        foreach(var rocker in _rockerList)
        {
            rocker.Begin(pos);
        }
    }

    public override void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        foreach (var rocker in _rockerList)
        {
            rocker.Drag(startPint, point, deltaPoint);
        }
    }

    public override void End(Vector2 point)
    {
        foreach (var rocker in _rockerList)
        {
            rocker.End(point);
        }
    }

}