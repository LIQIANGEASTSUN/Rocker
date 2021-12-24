using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputModelRocker : IInputSystem
{
    void ModelBegineMove(Vector3 dir);

    void ModelMoving(Vector3 dir);

    void ModelEndMove(Vector3 dir);
}
