using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullBlock : IBlock
{
    public int TouchHealth {  get; protected set; }

    public Color NewColor {  get; protected set; }

    public NullBlock(Color color)
    {
        TouchHealth = 1;
        NewColor = color;
    }

    public void Touch()
    {
        Debug.Log("I am NullBlock");
    }
}
