using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RockerAB : IRocker
{
    protected Rect _pickArea;
    protected Vector2 _startPosition;

    private int _workingFingerId = -1;
    public RockerAB()
    {
    }

    public void Init(Rect pickArea)
    {
        _pickArea = pickArea;
        RegisterEvent();
    }

    private void BeginDrag(int fingerId, Vector2 position)
    {
        if (_workingFingerId >= 0 && _workingFingerId != fingerId)
        {
            return;
        }
        if (!_pickArea.Contains(position))
        {
            return;
        }
        _workingFingerId = fingerId;
        _startPosition = position;
        Begin(position);
    }

    private void Drag(int fingerId, Vector2 position, Vector2 deltaPosition)
    {
        if (_workingFingerId != fingerId)
        {
            return;
        }
        Drag(_startPosition, position, deltaPosition);
    }

    private void DragEnd(int fingerId, Vector2 position)
    {
        if (_workingFingerId != fingerId)
        {
            return;
        }
        End(position);
        _workingFingerId = -1;
    }

    public abstract void Begin(Vector2 pos);

    public abstract void Drag(Vector2 startPint, Vector2 point, Vector2 deltaPoint);

    public abstract void End(Vector2 point);

    private void RegisterEvent()
    {
        FingerGestureSystem.GetInstance().fingerTouchBeginDrag += BeginDrag;
        FingerGestureSystem.GetInstance().fingerTouchDrag += Drag;
        FingerGestureSystem.GetInstance().fingerTouchDragEnd += DragEnd;
    }

    private void UnRegisterEvent()
    {
        FingerGestureSystem.GetInstance().fingerTouchBeginDrag -= BeginDrag;
        FingerGestureSystem.GetInstance().fingerTouchDrag -= Drag;
        FingerGestureSystem.GetInstance().fingerTouchDragEnd -= DragEnd;
    }

    public void Release()
    {
        _workingFingerId = -1;
        UnRegisterEvent();
    }

}
