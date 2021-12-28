using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerModelRotatePlane : MonoBehaviour
{
    private UIModelRockerRotate _uIModelRockerRotate;

    void Start()
    {
        _uIModelRockerRotate = new UIModelRockerRotate(transform);
        //// 控制人转向
        RockerUIModelRotate rockerUIModelRotate = new RockerUIModelRotate();
        _uIModelRockerRotate.AddRocker(rockerUIModelRotate);
    }

    public void Update()
    {
        _uIModelRockerRotate.Update();
    }
}
