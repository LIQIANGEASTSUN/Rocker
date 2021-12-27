using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerRoleDirectionBtn : MonoBehaviour
{
    private RockerRoleDirectionController _rockerRoleDirectionController;

    void Start()
    {
        _rockerRoleDirectionController = new RockerRoleDirectionController(transform);
        CameraFollowUseTargetToward _cameraFollowUseTargetToward = new CameraFollowUseTargetToward();
        //// 控制人转向
        RockerRoleDirection rockerRoleDirection = new RockerRoleDirection(_cameraFollowUseTargetToward);
        _rockerRoleDirectionController.AddRocker(rockerRoleDirection);
    }

    public void Update()
    {
        _rockerRoleDirectionController.Update();
    }
}
