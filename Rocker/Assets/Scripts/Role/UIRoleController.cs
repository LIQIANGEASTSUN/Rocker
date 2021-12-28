using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoleController : SingletonObject<UIRoleController>
{
    private Transform _roleTr;
    private float _rotateSpeed = 0.8f;

    public UIRoleController()
    {
        _roleTr = GameObject.Find("Role").transform;
    }

    public void Rotate(Quaternion quaternion)
    {
        _roleTr.rotation = quaternion;
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
