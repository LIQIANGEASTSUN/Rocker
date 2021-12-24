using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制摄像机旋转(不改变UI、按钮位置)
/// </summary>
public class RockCameraWidget
{
    private IRock _iRock;
    private RockBase _rockBase;

    public void Start()
    {
        _iRock = new RockCamera();
        _rockBase = new RockBase();

        Vector2 minPickPos = new Vector2(Screen.width * 0.5f, 0);
        Vector2 pickSize = new Vector2(Screen.width * 0.5f, Screen.height);
        Vector2 minActivePos = new Vector2(0, 0);
        Vector2 activeSize = new Vector2(Screen.width, Screen.height);

        _rockBase.Init(null, _iRock, minPickPos, pickSize, minActivePos, activeSize);
        _rockBase.SetGroup(RockGroupEnum.CAMERA_ROCK);
    }

    public void Update()
    {
        _rockBase.Update();
    }
}

/// <summary>
/// 控制UI控件位置
/// </summary>
public class RockUIWidget
{
    private RockBase _rockBase;

    public void Init(Transform transform, string targetName, int width, List<string> ignores)
    {
        _rockBase = new RockBase();

        RectTransform target = transform.Find(targetName).GetComponent<RectTransform>();
        Vector2 targetScreenPos = Utility.PositionConvert.UIPositionToScreenPosition(target.position);

        Vector2 pickCenter = targetScreenPos;
        float pickWidth = target.rect.width;
        float pickHeight = target.rect.height;

        // 这里 Active 区域 = Pick 区域
        _rockBase.Init(target, null, pickCenter, pickWidth, pickHeight, pickCenter, pickWidth, pickHeight);
        _rockBase.TargetAreaRadiu(width);
        _rockBase.SetGroup(RockGroupEnum.CAMERA_ROCK);
        //_rockBase.InitDrawArea();
    }

    public void Update()
    {
         _rockBase.Update();
    }
}