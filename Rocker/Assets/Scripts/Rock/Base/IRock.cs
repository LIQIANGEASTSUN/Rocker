using UnityEngine;

public interface IRock
{
    void Begin(Vector2 pos);
    void Move(Vector2 pos, float percentage);
    void End(Vector2 pos);
}


public interface IRocker
{
    void Begin(Vector2 pos);

    void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint);

    void End(Vector2 point);

}