using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTools
{

    public static float Angle(Vector3 from, Vector3 to, ref Vector3 cross)
    {
        float angle = Vector3.Angle(from, to);
        cross = Vector3.Cross(from, to);
        return angle;
    }

    public static float AngleUI(Vector3 from, Vector3 to)
    {
        Vector3 cross = Vector3.zero;
        float angle = Angle(from, to, ref cross);
        int sign = cross.z > 0 ? 1 : -1;
        return angle * sign;
    }

    public static float AngleModel(Vector3 from, Vector3 to)
    {
        Vector3 cross = Vector3.zero;
        float angle = Angle(from, to, ref cross);
        int sign = cross.y > 0 ? 1 : -1;
        return angle * sign;
    }

}
