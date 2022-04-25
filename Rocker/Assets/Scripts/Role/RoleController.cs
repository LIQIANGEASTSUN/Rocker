using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController : SingletonObject<RoleController>
{
    private ICameraFollow _cameraFollow;
    private Transform _roleTr;
    private float _moveSpeed = 1.5f;
    private float _rotateSpeed = 0.2f;
    private bool _move = false;
    private Vector3 _moveDir;

    // 设置正方向,也是摄像机朝向前方在水平面上的投影向量
    private Vector3 _worldForward = new Vector3(0, 0, 1);

    public RoleController()
    {
        _roleTr = GameObject.Find("Role").transform;
    }

    public void Update()
    {
        if (_move)
        {
            _roleTr.Translate(_moveDir * _moveSpeed * Time.deltaTime, Space.World);
        }
    }

    public void LateUpdate()
    {
        if (null != _cameraFollow)
        {
            _cameraFollow.Move();
        }
    }

    public void AddCameraFollow(ICameraFollow cameraFollow)
    {
        _cameraFollow = cameraFollow;
        _cameraFollow.SetInfo(_worldForward);
    }

    public void Move(Vector3 dir)
    {
        _move = true;
        _moveDir = dir;
    }

    public void MoveEnd()
    {
        _move = false;
    }

    public void Rotate(Quaternion quaternion)
    {
        _roleTr.rotation = quaternion;
    }

    public Vector3 WorldForward
    {
        get { return _worldForward; }
    }

    public Vector3 RoleForward
    {
        get { return _roleTr.forward; }
    }

    public Vector3 RolePosition
    {
        get { return _roleTr.position; }
    }

    public Quaternion RoleRotate
    {
        get { return _roleTr.rotation; }
    }
    
    public float RotateSpeed
    {
        get { return _rotateSpeed; }
    }

}
