using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockerRolerArrow : IRock
{
    private RectTransform _rockDirection;
    // 拖拽时指示方向的箭头移动距离范围
    private float _rockDirecRange;

    public RockerRolerArrow(Transform rockerBtnTr)
    {
        _rockDirection = TransformHelper.GetChild(rockerBtnTr, "RockerDirection").GetComponent<RectTransform>();
        RectTransform targetBg = rockerBtnTr.Find("RockerBackground").GetComponent<RectTransform>();
        _rockDirecRange = (targetBg.rect.width * 0.5f) + 10f;

    }

    public void Begin(Vector2 pos)
    {

    }

    public void Move(Vector2 pos, float percentage)
    {
        Vector3 dir = (Vector3)(pos.normalized);
        _rockDirection.localPosition = dir * _rockDirecRange;
        Quaternion quart = Quaternion.FromToRotation(Vector3.up, dir);
        _rockDirection.localRotation = quart;
        if (!_rockDirection.gameObject.activeSelf)
            _rockDirection.gameObject.SetActive(true);
    }

    public void End(Vector2 pos)
    {
        _rockDirection.gameObject.SetActive(false);
    }
}
