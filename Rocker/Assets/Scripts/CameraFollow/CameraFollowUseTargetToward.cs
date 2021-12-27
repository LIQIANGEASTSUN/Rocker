using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowUseTargetToward : ICameraFollow
{
    private Camera _camera;
    private Transform _cameraTr;
    private Transform _target;
    private float _distance = 5;
    private float _hight = 5;
    public CameraFollowUseTargetToward()
    {
        _camera = Camera.main;
        _cameraTr = _camera.transform;
    }

    public void SetInfo(Transform target, Vector3 defaultForward)
    {
        _target = target;
    }

    public void Move()
    {
        Vector3 position = (_target.forward * -1) * _distance + Vector3.up * _hight;
        _cameraTr.position = _target.position + position;
        _cameraTr.LookAt(_target);
    }
}
