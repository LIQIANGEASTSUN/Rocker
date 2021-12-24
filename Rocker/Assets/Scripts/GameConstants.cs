using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants
{
    //预设分辨率
    public static Vector2 GAME_RESOLUTION = new Vector2(1136f, 640f);

    #region GameInput KEY
    /// <summary>
    /// 操作人的摇杆 
    /// </summary>
    public const int SPRITE_ROCK_BUTTON = 1001;
    /// <summary>
    ///  操作摄像机的摇杆
    /// </summary>
    public const int CAMERA_ROCK_BUTTON = 1002;   // 
    /// <summary>
    /// 变鱼按钮
    /// </summary>
    public const int TRANSFORM_BUTTON = 1003;   // 

    /// <summary>
    /// 跳跃按钮
    /// </summary>
    public const int JUMP_BUTTON = 1008;
    /// <summary>
    /// 重置摄像机视角按钮
    /// </summary>
    public const int RESET_CAMERA_BUTTON = 1009;
    #endregion

    #region GameInput MSG
    public const int BUTTON_INVALID = 100;
    public const int BUTTON_DOWN = 101;    // 按钮按下
    public const int BUTTON_PRESS = 102;    // 按钮长按
    public const int BUTTON_UP = 103;    // 按钮抬起
    #endregion
}