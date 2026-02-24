using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{
    [SerializeField] private ColorUI _colorUI;
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
                    CreateColorForBox();
                }
            }
        }
    }

    private Vector3[] GetGrid(int size)
    {
        Vector3[] grid;

        _blocks = new List<BlockColor>();

        switch (size)
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

        return grid;
    }

    private void GenerateNewBox()
    {
        Vector3[] grid = GetGrid(_coreGameManager.CountBoxForLevel);

        _blocks = new List<BlockColor>();

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

    private void UpdateBox()
    {
        ColorObject correct = _coreGameManager.ColorObjects[_coreGameManager.ColorObjects.Count - 1];

        _colorUI.UpdateText(correct);

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

    private void CreateColorForBox() //метод на первый запуск уровн€
    {
        if (_coreGameManager.Level != _level)
        {
            for (int i = 0; i < _blocks.Count; i++)
            {
                ColorObject randomColor = _coreGameManager.ColorObjects[Random.Range(0, _coreGameManager.ColorObjects.Count)];

                if (i < _coreGameManager.ColorObjects.Count) //«адать минимум по 1 блоку каждого из доступных цветов
                {
                    randomColor = _coreGameManager.ColorObjects[i];
                }

                _blocks[i].Decorate(new NullBlock(randomColor.Color));
            }
        }

        UpdateBox();
    }
}
