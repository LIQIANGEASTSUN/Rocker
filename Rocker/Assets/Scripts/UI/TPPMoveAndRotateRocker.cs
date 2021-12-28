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
    private RoleRocker _roleRocker;

    void Start()
    {
        _roleRocker = new RoleRocker(transform);

        // 控制人转向 
        RockerRotateController rockerRotateController = new RockerRotateController();
        _roleRocker.AddRocker(rockerRotateController);
        // 控制人移动
        RockerMoveForwardController rockerMoveForwardController = new RockerMoveForwardController();
        _roleRocker.AddRocker(rockerMoveForwardController);
        // 控制摇杆按钮上的指示方向的箭头
        RolerRockerArrow rolerRockerArrow = new RolerRockerArrow(transform);
        _roleRocker.AddRocker(rolerRockerArrow);

        CamerFollowPositionLockDirection camerFollowPositionLockDirection = new CamerFollowPositionLockDirection();
        RoleController.GetInstance().AddCameraFollow(camerFollowPositionLockDirection);
    }

    public void Update()
    {
        _roleRocker.Update();
    }
}