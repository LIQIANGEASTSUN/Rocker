using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerModelRotatePlane : MonoBehaviour
{
    private RockerUIModelRotateController _rockerUIModelRotateController;

    void Start()
    {
        _rockerUIModelRotateController = new RockerUIModelRotateController(transform);
        //// 控制人转向
        RockerUIModelRotate rockerUIModelRotate = new RockerUIModelRotate();
        _rockerUIModelRotateController.AddRocker(rockerUIModelRotate);
    }

    public void Update()
    {
        _rockerUIModelRotateController.Update();
    }
}
