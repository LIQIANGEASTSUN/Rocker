using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆操作UI上模型转动
/// </summary>
public class RockerUIModelRotateController : IRock
{
    private RockBase _rockBase;

    // 摇杆随手移动的小球移动距离范围
    private float _targetAreaRadius = 0;

    private List<IRock> _rockList = new List<IRock>();
    public RockerUIModelRotateController(Transform rotatePlane)
    {
        Init(rotatePlane);
    }

    public void AddRocker(IRock rock)
    {
        _rockList.Add(rock);
    }

    private void Init(Transform rotatePlane)
    {
        RectTransform targetBg = rotatePlane.Find("BG").GetComponent<RectTransform>();
        RectTransform min = rotatePlane.Find("BG/Min").GetComponent<RectTransform>();
        RectTransform max = rotatePlane.Find("BG/Max").GetComponent<RectTransform>();

        Vector2 minPos = Utility.PositionConvert.UIPositionToScreenPosition(min.position);
        Vector2 maxPos = Utility.PositionConvert.UIPositionToScreenPosition(max.position);

        // 设置开始拖拽区域为：从屏幕中间左下角到屏幕右边之间的位置
        // 只要是在左侧屏幕开始拖拽，都能产生拖拽效果
        Vector2 minPickPos = minPos;
        Vector2 pickSize = maxPos - minPos;
        Vector2 minActivePos = minPickPos;
        // 设置拖拽过程中手指在activeSize范围内都可以拖动
        Vector2 activeSize = pickSize; // new Vector2(Screen.width, Screen.height);
        _targetAreaRadius = targetBg.rect.width * 0.5f;

        _rockBase = new RockBase();
        _rockBase.Init(null, this, minPickPos, pickSize, minActivePos, activeSize);
        _rockBase.TargetAreaRadiu(_targetAreaRadius);
        _rockBase.SetGroup(RockGroupEnum.UIMODEL_ROCK);
    }

    public void Update()
    {
        _rockBase.Update();
    }

    public void Begin(Vector2 pos)
    {
        foreach (var rocker in _rockList)
        {
            rocker.Begin(pos);
        }
    }

    public void Move(Vector2 pos, float percentage)
    {
        percentage = Mathf.Max(percentage, 0);
        if (pos.sqrMagnitude > 0 && percentage > 0)
        {
            foreach (var rocker in _rockList)
            {
                rocker.Move(pos, percentage);
            }
        }
    }

    public void End(Vector2 pos)
    {
        foreach (var rocker in _rockList)
        {
            rocker.End(pos);
        }
    }

}
