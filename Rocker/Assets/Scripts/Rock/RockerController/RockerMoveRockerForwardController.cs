using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆操作人移动,向摇杆操作方向移动
/// </summary>
public class RockerMoveRockerForwardController : IRock
{
    public RockerMoveRockerForwardController()
    {
    }

    public void Begin(Vector2 pos)
    {

    }

    public void Move(Vector2 pos, float percentage)
    {
        Vector3 dir = (Vector3)(pos.normalized);
        // Unity 内物体饶某个轴旋转遵循左手定理：握拳伸直大拇指(令大拇指指向旋转轴正方向)
        // 其他四指，指向的就是旋转的正方向

        // 计算摇杆方向，在UI上朝向屏幕内的是Z轴，根据左手定理，
        // 因为我们是从 Z 轴负方向 朝 Z轴正方向看的所以
        // 顺时针拖拽摇杆时在UI 上是 Z 轴的负方向，
        // 逆时针拖拽摇杆时在UI 上是 Z 轴的正方向
        // 顺时针为负，逆时针为正
        float rockerAngle = AngleUI(Vector3.up, dir);

        // 计算人应该旋转的方向
        // 因为我们看向角色时是从上往下看，就是从Y轴的正方向朝Y轴负方向看
        // 顺指针旋转即为向Y轴的正方向旋转
        // 逆时针旋转即为向Y轴的负方向旋转
        // 顺时针为正，逆时针为负
        // 发现跟UI是反着来的，就是因为UI是从Z轴负向朝正向看，看人时是从Y轴正向朝Y轴负向看

        // 先让旋转角度乘以 -1 反转过来
        rockerAngle *= -1;

        // 场景中人有一个设置的正向(_forward),比如以Z轴正向为前方 _forward=(0, 0, 1)，或者以X轴负向为前方 _forward=(-1, 0, 0)
        // 因为都是旋转的Y轴，所以不管以X/Z哪个轴为正向，计算时使用的是相同的 rockerAngle 值

        // 人最终朝向 direction = _forward 绕 Y轴旋转 rockerAngle 所得
        Quaternion quaternion = Quaternion.AngleAxis(rockerAngle, Vector3.up);
        Vector3 direction = quaternion * RoleController.GetInstance().RoleForward;
        RoleController.GetInstance().Move(direction);
    }

    public void End(Vector2 pos)
    {

    }

    private float Angle(Vector3 from, Vector3 to, ref Vector3 cross)
    {
        float angle = Vector3.Angle(from, to);
        cross = Vector3.Cross(from, to);
        return angle;
    }

    private float AngleUI(Vector3 from, Vector3 to)
    {
        Vector3 cross = Vector3.zero;
        float angle = Angle(from, to, ref cross);
        int sign = cross.z > 0 ? 1 : -1;
        return angle * sign;
    }

}
