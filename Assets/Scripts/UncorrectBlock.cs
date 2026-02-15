using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncorrectBlock : Decorator
{
    public UncorrectBlock(IBlock block, Color color) : base(block, color)
    {
        TouchHealth = 2;
    }

    public override void Touch()
    {
        Debug.Log("Uncorrect block");

        base.Touch();
    }
}
