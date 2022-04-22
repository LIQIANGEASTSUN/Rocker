using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RockerAB 
{
    protected IRock _iRock;
    protected Rect _pickArea;
    protected Vector2 _startPosition;

    private int _workingFingerId = -1;
    public RockerAB()
    {

    }

    public void Init(IRock iRock, Rect pickArea)
    {
        _iRock = iRock;
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
        BeginDrag(position);
    }

    private void Drag(int fingerId, Vector2 position, Vector2 deltaPosition)
    {
        if (_workingFingerId != fingerId)
        {
            return;
        }
        Drag(position, deltaPosition);
    }

    private void DragEnd(int fingerId, Vector2 position)
    {
        if (_workingFingerId != fingerId)
        {
            return;
        }
        DragEnd(position);
    }

    protected abstract void BeginDrag(Vector2 position);

    protected abstract void Drag(Vector2 position, Vector2 deltaPosition);

    protected abstract void DragEnd(Vector2 position);

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
