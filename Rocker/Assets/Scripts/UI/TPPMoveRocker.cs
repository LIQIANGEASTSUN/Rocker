using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPMoveRocker : MonoBehaviour
{
    private RockerController _rockerController;

    void Start()
    {
        _rockerController = new RockerController(transform);

        RockerMoveRockerForward rockerMoveRockerForward = new RockerMoveRockerForward();
        _rockerController.AddRocker(rockerMoveRockerForward);

        CameraFollowPositionAndRotate cameraFollowPositionAndRotate = new CameraFollowPositionAndRotate();
        RoleController.GetInstance().AddCameraFollow(cameraFollowPositionAndRotate);

        // 控制摇杆按钮上的指示方向的箭头
        RockerRolerArrow _rockerRolerArrow = new RockerRolerArrow(transform);
        _rockerController.AddRocker(_rockerRolerArrow);

    }

    public void Update()
    {
        _rockerController.Update();
    }
}
