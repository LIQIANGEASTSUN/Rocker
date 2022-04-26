using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆上指示拖拽方向的小箭头
/// </summary>
public class RockerUIArrowReceive : IRocker
{
    // 摇杆中心的小球
    private RectTransform _rockerBallRT;
    // 摇杆上的指示箭头
    private RectTransform _rockDirection;

    private Vector2 _centerPoint;

    // 小球移动距离范围
    private float _rockerBallRange;
    // 拖拽时指示方向的箭头移动距离范围
    private float _rockDirecRange;

    public RockerUIArrowReceive(Transform rockerBtnTr)
    {
        _rockerBallRT = rockerBtnTr.Find("RockerBackground/Rocker").GetComponent<RectTransform>();
        _rockDirection = rockerBtnTr.Find("RockerBackground/RockerDirection").GetComponent<RectTransform>();

        _centerPoint = PositionConvert.UIPointToScreenPoint(rockerBtnTr.position);

        RectTransform maxTr = rockerBtnTr.Find("RockerBackground/Max").GetComponent<RectTransform>();
        RectTransform minTr = rockerBtnTr.Find("RockerBackground/Min").GetComponent<RectTransform>();

        Vector2 minPoint = PositionConvert.UIPointToScreenPoint(minTr.position);
        Vector2 maxPoint = PositionConvert.UIPointToScreenPoint(maxTr.position);

        float length = (maxPoint - minPoint).magnitude * 0.5f;
        _rockerBallRange = length * 0.46f;
        _rockDirecRange = length * 0.75f;
    }

    public void Begin(Vector2 pos)
    {
        DragRocker(pos, pos, Vector2.zero);
    }

    public void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        DragRocker(startPint, point, deltaPoint);
        DragDirection(startPint, point, deltaPoint);
    }

    private void DragRocker(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        Vector2 dir =(point - startPint).normalized;
        float magnitude = (point - startPint).magnitude;
        magnitude = Mathf.Clamp(magnitude, 0, _rockerBallRange);

        Vector2 screenPoint = _centerPoint + dir * magnitude;
        _rockerBallRT.position = PositionConvert.ScreenPointToUIPoint(_rockerBallRT, screenPoint);
    }

    private void DragDirection(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        Debug.LogError("Drag");
        Vector3 dir = (Vector3)(point - startPint).normalized;
        // 位置
        {
            Vector2 screenPoint = _centerPoint + new Vector2(dir.x * _rockDirecRange, dir.y * _rockDirecRange);
            _rockDirection.position = PositionConvert.ScreenPointToUIPoint(_rockDirection, screenPoint);
        }

        // 旋转
        {
            Quaternion quart = Quaternion.FromToRotation(Vector3.up, dir);
            _rockDirection.localRotation = quart;
            if (!_rockDirection.gameObject.activeSelf)
            {
                _rockDirection.gameObject.SetActive(true);
            }
        }
    }

    public void End(Vector2 pos)
    {
        DragRocker(pos, pos, Vector2.zero);
        _rockDirection.gameObject.SetActive(false);
    }


}
