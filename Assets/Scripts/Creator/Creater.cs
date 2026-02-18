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

        _blocks = new List<BlockColor>();

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

            _blocks.Add(block);
        }

        CreateColorForBox();
    }

    private void UpdateBox() //метод на запуски под уровней (промежутка между уровнями, когда не все блоки пропали с поля)
    {
        Debug.Log(_coreGameManager.ColorObjects.Count);

        ColorObject correct = _coreGameManager.ColorObjects[_coreGameManager.ColorObjects.Count - 1];

        _coreGameManager.UncorrectCount = 0;
        _coreGameManager.CorrectCount = 0;

        foreach (var block in _blocks)
        {
            if (block.gameObject.activeSelf)
            {
                if (block.Block.NewColor == correct.Color)
                {
                    block.Decorate(new CorrectBlock(block.Block));
                    _coreGameManager.CorrectCount++;
                }
                else
                {
                    block.Decorate(new UncorrectBlock(block.Block));
                    _coreGameManager.UncorrectCount++;
                }
            }
        }
    }

    private void CreateColorForBox() //метод на первый запуск уровня
    {
        Debug.Log(_coreGameManager.ColorObjects.Count);
        ColorObject correct = _coreGameManager.ColorObjects[_coreGameManager.ColorObjects.Count - 1];

        _coreGameManager.UncorrectCount = 0;
        _coreGameManager.CorrectCount = 0;

        for(int i = 0; i < _blocks.Count; i++)
        {
            ColorObject randomColor = _coreGameManager.ColorObjects[Random.Range(0, _coreGameManager.ColorObjects.Count)];

            if(i < _coreGameManager.ColorObjects.Count - 1) //Задать минимум по 1 блоку каждого из доступных цветов
            {
                randomColor = _coreGameManager.ColorObjects[i];
            }

            if (_blocks[i].gameObject.activeSelf)
            {
                if (randomColor.Color == correct.Color)
                {
                    _blocks[i].Decorate(new CorrectBlock(new NullBlock(randomColor.Color)));
                    _coreGameManager.CorrectCount++;
                }
                else
                {
                    _blocks[i].Decorate(new UncorrectBlock(new NullBlock(randomColor.Color)));
                    _coreGameManager.UncorrectCount++;
                }
            }
        }
    }
}
