using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : IBlock
{
    public int TouchHealth { get; protected set; }
    public Color NewColor { get; protected set; }

    protected IBlock _block;

    public Decorator(IBlock block, Color color)
    {
        _block = block;
        NewColor = color;
    }

    public virtual void Touch()
    {

    }
}
