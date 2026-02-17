using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BlockColor : MonoBehaviour
{
    public IBlock Block;

    public void Decorate(IBlock block)
    {
        Block = block;

        this.GetComponent<Renderer>().material.SetColor("_Color", Block.NewColor);
    }

    public void Touch()
    {
        Block.Touch();

        if(Block.TouchHealth <= 0)
        {
            GenericPool.Shared.pool.Release(this);
        }
    }
    public void Reset()
    {
        Block = new NullBlock(UnityEngine.Color.white);
    }
}
