using UnityEngine;


public interface IRocker
{
    void Begin(Vector2 pos);

    void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint);

    void End(Vector2 point);

}