using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputCameraRocker : IInputSystem
{
    void CameraBeginMove(Vector3 dir);

    void CameraMoving(Vector3 dir);

    void CameraEndMove(Vector3 dir);
}
