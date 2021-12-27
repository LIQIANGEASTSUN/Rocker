using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowFixedToWard
{
    private Camera _camera;
    private Transform _cameraTr;
    private Transform _target;
    private Vector3 _forward;
    private float _distance = 6;
    private float _hight = 3;
    public CameraFollowFixedToWard(Transform target, Vector3 forward)
    {
        _camera = Camera.main;
        _cameraTr = _camera.transform;
        _target = target;
        _forward = forward;
    }

    public void Move()
    {
        Vector3 position = (_forward * -1) * _distance + Vector3.up * _hight;
        _cameraTr.position = _target.position + position;
        _cameraTr.LookAt(_target);
    }

}
