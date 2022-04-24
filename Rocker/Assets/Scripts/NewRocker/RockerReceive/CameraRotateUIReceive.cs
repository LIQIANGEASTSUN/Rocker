using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateUIReceive : IRocker
{
    // 摇杆中心的小球
    private RectTransform _rockerBallRT;
    private Vector2 _centerPoint;

    // 小球移动距离范围
    private float _rockerBallRange;

    public CameraRotateUIReceive(Transform rockerBtnTr)
    {
        _rockerBallRT = rockerBtnTr.Find("RockBG").GetComponent<RectTransform>();
        _centerPoint = PositionConvert.UIPointToScreenPoint(rockerBtnTr.position);

        RectTransform maxTr = rockerBtnTr.Find("Max").GetComponent<RectTransform>();
        RectTransform minTr = rockerBtnTr.Find("Min").GetComponent<RectTransform>();

        Vector2 minPoint = PositionConvert.UIPointToScreenPoint(minTr.position);
        Vector2 maxPoint = PositionConvert.UIPointToScreenPoint(maxTr.position);

        float length = (maxPoint - minPoint).magnitude * 0.5f;
        _rockerBallRange = length * 0.46f;
    }

    public void Begin(Vector2 pos)
    {
        DragRocker(pos, pos, Vector2.zero);
    }

    public void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        DragRocker(startPint, point, deltaPoint);
    }

    private void DragRocker(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        Vector2 dir = (point - startPint).normalized;
        float magnitude = (point - startPint).magnitude;
        magnitude = Mathf.Clamp(magnitude, 0, _rockerBallRange);

        Vector2 screenPoint = _centerPoint + dir * magnitude;
        _rockerBallRT.position = PositionConvert.ScreenPointToUIPoint(_rockerBallRT, screenPoint);
    }

    public void End(Vector2 pos)
    {
        DragRocker(pos, pos, Vector2.zero);
    }

}
