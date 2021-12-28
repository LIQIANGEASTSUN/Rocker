using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraFollow
{
    void SetInfo(Vector3 defaultForward);

    void Move();
}
