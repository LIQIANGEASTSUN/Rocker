using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆控制人转向
/// </summary>
public class CameraRotateRocker : MonoBehaviour
{
    private RoleRotateRocker _roleRotateRocker;
    // Start is called before the first frame update
    void Start()
    {
        _roleRotateRocker = new RoleRotateRocker(transform);
    }

    private void OnDestroy()
    {
        _roleRotateRocker.Release();
    }

}

public class RoleRotateRocker : RockerAB
{
    private List<IRocker> _rockerList = new List<IRocker>();
    public RoleRotateRocker(Transform transform)
    {
        // 设置开始拖拽区域为：从屏幕中间左下角到屏幕右边之间的位置
        // 只要是在左侧屏幕开始拖拽，都能产生拖拽效果
        Vector2 minPickPos = new Vector2(Screen.width * 0.5f, 0);
        Vector2 pickSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Rect pickArea = new Rect(minPickPos, pickSize);
        Init(pickArea);

        // 控制摄像机旋转，并控制人朝向摄像机朝向
        CameraRotateReceive cameraRotateReceive = new CameraRotateReceive();
        _rockerList.Add(cameraRotateReceive);

        CameraRotateUIReceive cameraRotateUIReceive = new CameraRotateUIReceive(transform);
        _rockerList.Add(cameraRotateUIReceive);
    }

    public override void Begin(Vector2 pos)
    {
        foreach(var rocker in _rockerList)
        {
            rocker.Begin(pos);
        }
    }

    public override void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        foreach(var rocker in _rockerList)
        {
            rocker.Drag(_startPosition, point, deltaPoint);
        }
    }

    public override void End(Vector2 point)
    {
        foreach(var rocker in _rockerList)
        {
            rocker.End(point);
        }
    }
}
