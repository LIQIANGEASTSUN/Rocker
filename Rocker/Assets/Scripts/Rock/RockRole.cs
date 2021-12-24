using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRole:IRock  {

    public Queue<InputCache> inputQueue = new Queue<InputCache>();
    private float _maxPercentage = 1;
    public RockRole(float maxPercentage)
    {
        _maxPercentage = maxPercentage;
    }

    public void Begin(Vector2 pos)
    {
        InputCache input = new InputCache(GameConstants.SPRITE_ROCK_BUTTON, GameConstants.BUTTON_DOWN, Vector3.zero);
        inputQueue.Enqueue(input);
    }

    public void Move(Vector2 pos, float percentage)
    {
        if (percentage > 0.2f)
        {
            percentage = 1;
        }
        percentage = Mathf.Clamp(percentage, 0, _maxPercentage);
        Vector3 dir = (Vector3)(pos.normalized * percentage) / _maxPercentage;// (Vector3)(pos - beginPosition).normalized;
        if (dir.magnitude <= 0)
        {
            return;
        }

        InputCache input = new InputCache(GameConstants.SPRITE_ROCK_BUTTON, GameConstants.BUTTON_PRESS, dir);
        inputQueue.Enqueue(input);
    }

    public void End(Vector2 pos)
    {
        InputCache input = new InputCache(GameConstants.SPRITE_ROCK_BUTTON, GameConstants.BUTTON_UP, Vector3.zero);
        inputQueue.Enqueue(input);
    }

    public void FixedUpdate()
    {
        while (inputQueue.Count > 0)
        {
            InputCache input = inputQueue.Peek();
            //最后一个Press不要Dequeue，估计是为了下一次Update继续能产生Input事件
            if (inputQueue.Count == 1 && (input._msg == GameConstants.BUTTON_PRESS))
            {
                break;
            }

            inputQueue.Dequeue();
        }
    }
}
