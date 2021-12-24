using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct InputCache
{
    public int _key;
    public int _msg;
    public Vector3 _parameter;

    public InputCache(int key, int msg, Vector3 parameter)
    {
        _key = key;
        _msg = msg;
        _parameter = parameter;
    }
}