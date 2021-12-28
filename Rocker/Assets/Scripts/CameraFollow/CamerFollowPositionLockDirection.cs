using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机跟随人的位置，锁定摄像机朝向
/// </summary>
public class CamerFollowPositionLockDirection : ICameraFollow
{
    private Camera _camera;
    private Transform _cameraTr;
    private Vector3 _forward;
    private float _distance = 5;
    private float _hight = 5;

    public CamerFollowPositionLockDirection()
    {
        _camera = Camera.main;
        _cameraTr = _camera.transform;
    }

    public void SetInfo(Vector3 defaultForward)
    {
        _forward = defaultForward;
    }

    public void Move()
    {
        Vector3 position = (_forward * -1) * _distance + Vector3.up * _hight;
        Vector3 targetPosition = RoleController.GetInstance().RolePosition;
        _cameraTr.position = targetPosition + position;
        _cameraTr.LookAt(targetPosition);
    }
}
