using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{
    private CoreGameManager _coreGameManager;

    private List<BlockColor> _blocks = new List<BlockColor>();

    private int _level = 0;
    private void Start()
    {
        _coreGameManager = new CoreGameManager();

        GenerateNewBox();

        _level = _coreGameManager.Level;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                hit.collider.gameObject.GetComponent<BlockColor>().Touch();
                if(_coreGameManager.Level != _level)
                {
                    GenerateNewBox();
                    _level = _coreGameManager.Level;
                }
                else if(_coreGameManager.CorrectCount <= 0)
                {
                    UpdateBox();
                }
            }
        }
    }

    private void GenerateNewBox()
    {
        Vector3[] grid;

        switch (_coreGameManager.CountBoxForLevel)
        {
            case 3:
                grid = new Vector3[9]
                {
                    Vector3.left + Vector3.up , Vector3.up, Vector3.right + Vector3.up,
                    Vector3.left, Vector3.zero, Vector3.right,
                    Vector3.left + Vector3.down, Vector3.down, Vector3.right + Vector3.down
                };
                break;
            case 4:
                grid = new Vector3[16]
                {
                    new Vector3(-1.5f, 1.5f, 0), new Vector3(-0.5f, 1.5f, 0f), new Vector3(0.5f, 1.5f, 0), new Vector3(1.5f, 1.5f, 0),
                    new Vector3(-1.5f, 0.5f, 0), new Vector3(-0.5f, 0.5f, 0f), new Vector3(0.5f, 0.5f, 0), new Vector3(1.5f, 0.5f, 0),
                    new Vector3(-1.5f, -0.5f, 0), new Vector3(-0.5f, -0.5f, 0f), new Vector3(0.5f, -0.5f, 0), new Vector3(1.5f, -0.5f, 0),
                    new Vector3(-1.5f, -1.5f, 0), new Vector3(-0.5f, -1.5f, 0f), new Vector3(0.5f, -1.5f, 0), new Vector3(1.5f, -1.5f, 0),
                };
                break;
            default:
                grid = new Vector3[25]
                {
                    new Vector3(-2f, 2f, 0), new Vector3(-1f, 2f, 0f),new Vector3(0f, 2f, 0f), new Vector3(1f, 2f, 0), new Vector3(2f, 2f, 0),
                    new Vector3(-2f, 1f, 0), new Vector3(-1f, 1f, 0f),new Vector3(0f, 1f, 0f), new Vector3(1f, 1f, 0), new Vector3(2f, 1f, 0),
                    new Vector3(-2f, 0f, 0), new Vector3(-1f, 0f, 0f),new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0), new Vector3(2f, 0f, 0),
                    new Vector3(-2f, -1f, 0), new Vector3(-1f, -1f, 0f),new Vector3(0f, -1f, 0f), new Vector3(1f, -1f, 0), new Vector3(2f, -1f, 0),
                    new Vector3(-2f, -2f, 0), new Vector3(-1f, -2f, 0f),new Vector3(0f, -2f, 0f), new Vector3(1f, -2f, 0), new Vector3(2f, -2f, 0),
                };
                break;
        }

        BlockColor block;

        foreach (var pos in grid)
        {
            block = GenericPool.Shared.pool.Get();

            block.Reset();

            block.transform.position = pos;

            switch (Random.Range(0, 2))
            {
                case 0:
                    block.Decorate(new UncorrectBlock(new NullBlock(Color.red)));
                    _coreGameManager.UncorrectCount++;
                    break;
                case 1:
                    block.Decorate(new CorrectBlock(new NullBlock(Color.yellow)));
                    _coreGameManager.CorrectCount++;
                    break;
            }

            _blocks.Add(block);
        }
    }

    private void UpdateBox()
    {
        foreach (var block in _blocks)
        {
            if (block.gameObject.activeSelf)
            {
                block.Decorate(new CorrectBlock(block.Block));
                _coreGameManager.CorrectCount++;
                _coreGameManager.UncorrectCount--;
            }
        }

        _blocks = new List<BlockColor>();
    }
}
