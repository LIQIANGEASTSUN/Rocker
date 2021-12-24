using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCamera:IRock  {

    private IInputCameraRocker inputCameraRocker;

    public RockCamera()
    {
    }

    private Vector2 lastPosition = Vector2.zero;
    public void Begin(Vector2 pos)
    {
        if (null != inputCameraRocker)
        {
            inputCameraRocker.CameraBeginMove(Vector3.zero);
        }
        lastPosition = pos;
    }

    public void Move(Vector2 pos, float percentage)
    {
        Vector3 dir = (pos - lastPosition);
        if (null != inputCameraRocker)
        {
            inputCameraRocker.CameraMoving(dir);
        }
        if (dir.magnitude <= 0)
            return;
        lastPosition = pos;
    }

    public void End(Vector2 pos)
    {
        if (null != inputCameraRocker)
        {
            inputCameraRocker.CameraEndMove(Vector3.zero);
        }
        lastPosition = pos;
    }

}
