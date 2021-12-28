using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPMoveRocker : MonoBehaviour
{
    private RoleRocker _roleRocker;

    void Start()
    {
        _roleRocker = new RoleRocker(transform);

        RockerMoveRockerForwardController rockerMoveRockerForward = new RockerMoveRockerForwardController();
        _roleRocker.AddRocker(rockerMoveRockerForward);

        CameraFollowPositionAndRotate cameraFollowPositionAndRotate = new CameraFollowPositionAndRotate();
        RoleController.GetInstance().AddCameraFollow(cameraFollowPositionAndRotate);

        // 控制摇杆按钮上的指示方向的箭头
        RolerRockerArrow rolerRockerArrow = new RolerRockerArrow(transform);
        _roleRocker.AddRocker(rolerRockerArrow);
    }

    public void Update()
    {
        _roleRocker.Update();
    }
}
