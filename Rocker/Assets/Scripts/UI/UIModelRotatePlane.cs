using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModelRotatePlane : MonoBehaviour
{
    private UIModelRockerRotate _uIModelRockerRotate;

    void Start()
    {
        _uIModelRockerRotate = new UIModelRockerRotate(transform);
    }
}

public class UIModelRockerRotate : RockerAB
{
    private IRocker _uiModelRocker = null;
    public UIModelRockerRotate(Transform rotatePlane)
    {
        RectTransform targetBg = rotatePlane.Find("BG").GetComponent<RectTransform>();
        RectTransform min = rotatePlane.Find("BG/Min").GetComponent<RectTransform>();
        RectTransform max = rotatePlane.Find("BG/Max").GetComponent<RectTransform>();

        Vector2 minPos = PositionConvert.UIPointToScreenPoint(min.position);
        Vector2 maxPos = PositionConvert.UIPointToScreenPoint(max.position);

        Rect pickArea = new Rect(minPos, maxPos - minPos);
        Init(pickArea);

        _uiModelRocker = new UIModelRotateReceive();
    }

    public override void Begin(Vector2 pos)
    {
        _uiModelRocker.Begin(pos);
    }

    public override void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint)
    {
        _uiModelRocker.Drag(_startPosition, point, deltaPoint);
    }

    public override void End(Vector2 point)
    {
        _uiModelRocker.End(point);
    }

}