using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 操作移动摇杆
/// </summary>
public class RockerFixedTowardBtn : MonoBehaviour
{
    private RockerRoleController _rockerRoleController;

    void Start()
    {
        _rockerRoleController = new RockerRoleController(transform);
        CameraFollowFixedToWard _cameraFollowFixedToWard = new CameraFollowFixedToWard();
        // 控制人转向
        RockerRoleModelFixedToward _rockerRoleModelFixedToward = new RockerRoleModelFixedToward(_cameraFollowFixedToWard);
        _rockerRoleController.AddRocker(_rockerRoleModelFixedToward);
        // 控制摇杆按钮上的指示方向的箭头
        RockerRolerArrow _rockerRolerArrow = new RockerRolerArrow(transform);
        _rockerRoleController.AddRocker(_rockerRolerArrow);
    }

    public void Update()
    {
        _rockerRoleController.Update();
    }
}