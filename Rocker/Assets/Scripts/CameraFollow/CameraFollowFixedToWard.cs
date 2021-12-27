﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowFixedToWard : ICameraFollow
{
    private Camera _camera;
    private Transform _cameraTr;
    private Transform _target;
    private Vector3 _forward;
    private float _distance = 5;
    private float _hight = 5;

    public CameraFollowFixedToWard()
    {
        _camera = Camera.main;
        _cameraTr = _camera.transform;
    }

    public void SetInfo(Transform target, Vector3 defaultForward)
    {
        _target = target;
        _forward = defaultForward;
    }

    public void Move()
    {
        Vector3 position = (_forward * -1) * _distance + Vector3.up * _hight;
        _cameraTr.position = _target.position + position;
        _cameraTr.LookAt(_target);
    }

}
