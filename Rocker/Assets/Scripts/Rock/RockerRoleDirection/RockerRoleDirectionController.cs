using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 摇杆操作人转向
public class RockerRoleDirectionController : IRock
{
    private RockBase _rockBase;

    // 摇杆随手移动的小球移动距离范围
    private float _targetAreaRadius = 0;

    // 拖拽百分比:拖拽距离 / _targetAreaRadius 得值
    private float _maxPercentage = 1;

    private List<IRock> _rockList = new List<IRock>();
    public RockerRoleDirectionController(Transform rockerBtnTr)
    {
        Init(rockerBtnTr);
    }

    public void AddRocker(IRock rock)
    {
        _rockList.Add(rock);
    }

    private void Init(Transform rockerBtnTr)
    {
        // 随着手一动的小球
        RectTransform target = TransformHelper.GetChild(rockerBtnTr, "RockBG").GetComponent<RectTransform>();
        RectTransform targetBg = rockerBtnTr.Find("BG").GetComponent<RectTransform>();

        // 设置开始拖拽区域为：从屏幕中间左下角到屏幕右边之间的位置
        // 只要是在左侧屏幕开始拖拽，都能产生拖拽效果
        Vector2 minPickPos = new Vector2(Screen.width * 0.5f, 0);
        Vector2 pickSize = new Vector2(Screen.width, Screen.height);
        Vector2 minActivePos = minPickPos;
        // 设置拖拽过程中手指在activeSize范围内都可以拖动
        Vector2 activeSize = new Vector2(Screen.width, Screen.height);
        _targetAreaRadius = (targetBg.rect.width * 0.5f) - (target.rect.width * 0.5f) - 2f;

        _rockBase = new RockBase();
        _rockBase.Init(target, this, minPickPos, pickSize, minActivePos, activeSize);
        _rockBase.TargetAreaRadiu(_targetAreaRadius);
        _rockBase.SetGroup(RockGroupEnum.CAMERA_ROCK);
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
        percentage = Mathf.Clamp(percentage, 0, _maxPercentage);
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
