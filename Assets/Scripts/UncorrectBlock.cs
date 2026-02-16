using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncorrectBlock : Decorator
{
    public static event Action<IBlock> OnTouchUncorrect;
    public UncorrectBlock(IBlock block, Color color) : base(block, color)
    {
        TouchHealth = 2;
    }

    public override void Touch()
    {
        Debug.Log("Uncorrect block");

        TouchHealth--;

        if(TouchHealth <= 0)
        {
            OnTouchUncorrect.Invoke(_block);
        }

        base.Touch();
    }
}
