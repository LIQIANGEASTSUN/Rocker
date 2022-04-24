using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPMoveRocker : MonoBehaviour
{
    private MoveRocker _moveRocker;

    void Start()
    {
        _moveRocker = new MoveRocker(transform);

        CameraFollowPositionAndRotate cameraFollowPositionAndRotate = new CameraFollowPositionAndRotate();
        RoleController.GetInstance().AddCameraFollow(cameraFollowPositionAndRotate);
    }

    private void OnDestroy()
    {
        _moveRocker.Release();
    }

}

public class MoveRocker : RockerAB
{
    private List<IRocker> _rockerList = new List<IRocker>();
    public MoveRocker(Transform transform)
    {
        // 设置开始拖拽区域为：从屏幕左下角到屏幕正中间之间的位置
        // 只要是在左侧屏幕开始拖拽，都能产生拖拽效果
        Vector2 minPickPos = new Vector2(0, 0);
        Vector2 pickSize = new Vector2(Screen.width * 0.5f, Screen.height);
        Rect pickArea = new Rect(minPickPos, pickSize);
        Init(pickArea);

        RoleMoveReceive roleMoveReceive = new RoleMoveReceive();
        _rockerList.Add(roleMoveReceive);

        // 控制摇杆按钮上的指示方向的箭头
        RockerUIArrowReceive rockerUIArrowReceive = new RockerUIArrowReceive(transform);
        _rockerList.Add(rockerUIArrowReceive);
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
        foreach (var rocker in _rockerList)
        {
            rocker.Drag(_startPosition, point, deltaPoint);
        }
    }

    public override void End(Vector2 point)
    {
        foreach (var rocker in _rockerList)
        {
            rocker.End(point);
        }
    }

}