using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆控制人转向
/// </summary>
public class TTPRotateRocker : MonoBehaviour
{
    private RoleRockerDirection _roleRockerDirection;
    // Start is called before the first frame update
    void Start()
    {
        _roleRockerDirection = new RoleRockerDirection(transform);

        // 控制人转向 
        RockerDragRotateController rockerDragRotateController = new RockerDragRotateController();
        _roleRockerDirection.AddRocker(rockerDragRotateController);
    }

    // Update is called once per frame
    void Update()
    {
        _roleRockerDirection.Update();
    }
}
