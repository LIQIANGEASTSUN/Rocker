using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerModelRotatePlane : MonoBehaviour
{
    private UIModelRockerRotateInput _uIModelRockerRotateInput;

    void Start()
    {
        _uIModelRockerRotateInput = new UIModelRockerRotateInput(transform);
        //// 控制人转向
        RockerUIModelRotateReceive rockerUIModelRotateReceive = new RockerUIModelRotateReceive();
        _uIModelRockerRotateInput.AddRocker(rockerUIModelRotateReceive);
    }

    public void Update()
    {
        _uIModelRockerRotateInput.Update();
    }
}
