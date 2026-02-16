using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectBlock : Decorator
{
    public static event Action<IBlock> OnTouchCorrect;
    public CorrectBlock(IBlock block, Color color) : base(block, color)
    {
        TouchHealth = 1;
    }

    public override void Touch()
    {
        Debug.Log("Correct block");

        TouchHealth--;

        OnTouchCorrect.Invoke(_block);

        base.Touch();
    }
}
