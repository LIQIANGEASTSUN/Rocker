using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Camera;

public class PositionConvert
{

    public static Vector2 WorldPointToScreenPoint(Vector3 worldPoint)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);
        return screenPoint;
    }

    public static Vector2 UIPointToScreenPoint(Vector3 worldPoint)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(UICamera.Instance.Camera, worldPoint);
        return screenPoint;
    }

    public static Vector3 ScreenPointToWorldPoint(Vector2 screenPoint, float planeZ)
    {
        Vector3 position = new Vector3(screenPoint.x, screenPoint.y, planeZ);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(position);
        return worldPoint;
    }

    public static Vector3 ScreenPointToUIPoint(RectTransform rt, Vector2 screenPoint)
    {
        Vector3 globalMousePos;
        //UI屏幕坐标转换为世界坐标
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, screenPoint, UICamera.Instance.Camera, out globalMousePos);
        //设置位置及偏移量
        //rt.transform.position = globalMousePos;
        return globalMousePos;
    }

    public static Vector2 ScreenPointToUILocalPoint(Vector2 screenPoint)
    {
        Vector2 localPos;
        RectTransform canvasRect = UICamera.Instance.Camera.transform.parent.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, UICamera.Instance.Camera, out localPos);
        //rt.anchoredPosition = localPos;
        return localPos;
    }

}
