using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GenericPool : MonoBehaviour
{
    public static GenericPool Shared;

    public BlockColor colorObject;
    public int defaultCapacity = 9;
    public int maxCapacity = 25;
    public bool collectionChect = true;

    private object _poolLock = new object();

    private IObjectPool<BlockColor> _pool;
    public IObjectPool<BlockColor> pool
    {
        get
        {
            if( _pool == null)
            {
                lock (_poolLock)
                {
                    _pool = new ObjectPool<BlockColor>
                        (
                        CreateColorObject,
                        TakeFromPool,
                        ReturnToPool,
                        DestroyColorObject,
                        collectionChect,
                        defaultCapacity,
                        maxCapacity
                        );
                }
            }

            return _pool;
        }
    }

    private BlockColor CreateColorObject()
    {
        BlockColor block = Instantiate(colorObject);
        block.gameObject.SetActive(false);
        block.transform.SetParent(this.transform);

        return block;
    }

    private void TakeFromPool(BlockColor block)
    {
        block.gameObject.SetActive(true);
    }

    private void ReturnToPool(BlockColor block)
    {
        block.gameObject.SetActive(false);
        block.Reset();
    }

    private void DestroyColorObject(BlockColor block)
    {
        Destroy(block.gameObject);
    }

    private void Awake()
    {
        Shared = this;
    }
}
