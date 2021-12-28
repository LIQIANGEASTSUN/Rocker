using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTPRotateRocker : MonoBehaviour
{
    private RockerRoleDirectionController _rockerRoleDirectionController;
    // Start is called before the first frame update
    void Start()
    {
        _rockerRoleDirectionController = new RockerRoleDirectionController(transform);

        // 控制人转向 
        RockerRotateController rockerRotateController = new RockerRotateController();
        _rockerRoleDirectionController.AddRocker(rockerRotateController);
    }

    // Update is called once per frame
    void Update()
    {
        _rockerRoleDirectionController.Update();
    }
}
