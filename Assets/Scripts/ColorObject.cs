using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject
{
    public string Name { get; private set; }
    public Color Color { get; private set; }
    public ColorObject(string ColorName, Color color)
    {
        Name = ColorName;
        Color = color;
    }
}
