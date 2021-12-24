using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 操作移动摇杆
/// </summary>
public class RockerBtn : MonoBehaviour
{
    private RockerRoleController _rockerRoleController;

    void Start()
    {
        _rockerRoleController = new RockerRoleController(transform);
    }

    public void Update()
    {
        _rockerRoleController.Update();
    }
}