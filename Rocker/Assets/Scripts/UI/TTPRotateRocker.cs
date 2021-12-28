using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆控制人转向
/// </summary>
public class TTPRotateRocker : MonoBehaviour
{
    private RockerRoleDirectionController _rockerRoleDirectionController;
    // Start is called before the first frame update
    void Start()
    {
        _rockerRoleDirectionController = new RockerRoleDirectionController(transform);

        // 控制人转向 
        RockerDragRotateController rockerDragRotateController = new RockerDragRotateController();
        _rockerRoleDirectionController.AddRocker(rockerDragRotateController);
    }

    // Update is called once per frame
    void Update()
    {
        _rockerRoleDirectionController.Update();
    }
}
