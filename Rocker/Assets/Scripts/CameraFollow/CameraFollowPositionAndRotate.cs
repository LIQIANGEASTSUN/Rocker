using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机跟随人的位置和转向，摄像机一直在人后方跟随
/// </summary>
public class CameraFollowPositionAndRotate : ICameraFollow
{
    private Camera _camera;
    private Transform _cameraTr;
    private float _distance = 5;
    private float _hight = 5;
    public CameraFollowPositionAndRotate()
    {
        _camera = Camera.main;
        _cameraTr = _camera.transform;
    }

    public void SetInfo(Vector3 defaultForward)
    {
    }

    public void Move()
    {
        Vector3 forward = RoleController.GetInstance().RoleForward;
        Vector3 position = (forward * -1) * _distance + Vector3.up * _hight;
        Vector3 targetPosition = RoleController.GetInstance().RolePosition;
        _cameraTr.position = targetPosition + position;
        _cameraTr.LookAt(targetPosition);
    }
}
