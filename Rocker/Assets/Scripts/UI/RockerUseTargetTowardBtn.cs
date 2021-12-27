using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerUseTargetTowardBtn : MonoBehaviour
{
    private RockerRoleController _rockerRoleController;

    void Start()
    {
        _rockerRoleController = new RockerRoleController(transform);
        CameraFollowUseTargetToward _cameraFollowUseTargetToward = new CameraFollowUseTargetToward();
        // 控制人转向
        RockerRoleMove _rockerRoleMove = new RockerRoleMove(_cameraFollowUseTargetToward);
        _rockerRoleController.AddRocker(_rockerRoleMove);
        // 控制摇杆按钮上的指示方向的箭头
        RockerRolerArrow _rockerRolerArrow = new RockerRolerArrow(transform);
        _rockerRoleController.AddRocker(_rockerRolerArrow);
    }

    public void Update()
    {
        _rockerRoleController.Update();
    }
}
