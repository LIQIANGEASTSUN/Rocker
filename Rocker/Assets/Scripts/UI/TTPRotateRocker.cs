using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆控制人转向
/// </summary>
public class TTPRotateRocker : MonoBehaviour
{
    private RoleRockerDirectionInput _roleRockerDirectionInput;
    // Start is called before the first frame update
    void Start()
    {
        _roleRockerDirectionInput = new RoleRockerDirectionInput(transform);

        // 控制人转向 
        RockerDragRotateController rockerDragRotateController = new RockerDragRotateController();
        _roleRockerDirectionInput.AddRocker(rockerDragRotateController);
    }

    // Update is called once per frame
    void Update()
    {
        _roleRockerDirectionInput.Update();
    }
}
