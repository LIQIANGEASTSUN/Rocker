using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPMoveRocker : MonoBehaviour
{
    private RoleRockerInput _roleRockerInput;

    void Start()
    {
        _roleRockerInput = new RoleRockerInput(transform);

        RockerMoveRockerForwardController rockerMoveRockerForward = new RockerMoveRockerForwardController();
        _roleRockerInput.AddRocker(rockerMoveRockerForward);

        CameraFollowPositionAndRotate cameraFollowPositionAndRotate = new CameraFollowPositionAndRotate();
        RoleController.GetInstance().AddCameraFollow(cameraFollowPositionAndRotate);

        // 控制摇杆按钮上的指示方向的箭头
        RolerRockerArrowReceive rolerRockerArrowReceive = new RolerRockerArrowReceive(transform);
        _roleRockerInput.AddRocker(rolerRockerArrowReceive);
    }

    public void Update()
    {
        _roleRockerInput.Update();
    }
}
