using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BlockColor : MonoBehaviour
{
    private IBlock _block;

    public void Decorate(IBlock block)
    {
        _block = block;

        this.GetComponent<Renderer>().material.SetColor("_Color", _block.NewColor);
    }

    public void Touch()
    {
        _block.Touch();
    }
    public void Reset()
    {
        _block = new NullBlock();
    }
}
