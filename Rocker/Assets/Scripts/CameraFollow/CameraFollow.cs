using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraFollow
{
    void SetInfo(Transform target, Vector3 defaultForward);

    void Move();
}
