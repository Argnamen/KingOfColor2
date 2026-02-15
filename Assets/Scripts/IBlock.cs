using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlock
{
    public int TouchHealth { get; }
    public Color NewColor { get; }

    public void Touch();
}
