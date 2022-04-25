using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager
{
    public static UIManager Instance;
    public Camera UICamera;

    private UIManager()
    {
        Canvas canvas = GameObject.Find("UIRoot").GetComponent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera
            || canvas.renderMode == RenderMode.WorldSpace)
        {
            UICamera = canvas.worldCamera;
        }
        else if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            UICamera = null;
        }
    }

    public static UIManager GetInstance()
    {
        if (null == Instance)
        {
            Instance = new UIManager();
        }
        return Instance;
    }
}