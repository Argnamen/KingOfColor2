using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectBlock : Decorator
{
    public CorrectBlock(IBlock block, Color color) : base(block, color)
    {
        TouchHealth = 1;
    }

    public override void Touch()
    {
        Debug.Log("Correct block");



        base.Touch();
    }
}
