using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{
    private void Start()
    {
        Vector3[] grid = new Vector3[9]
        {
            Vector3.left + Vector3.up , Vector3.up, Vector3.right + Vector3.up,
            Vector3.left, Vector3.zero, Vector3.right,
            Vector3.left + Vector3.down, Vector3.down, Vector3.right + Vector3.down
        };
        BlockColor block;

        foreach(var pos in grid)
        {
            block = GenericPool.Shared.pool.Get();

            block.Reset();

            block.transform.position = pos;

            switch (Random.Range(0, 2))
            {
                case 0:
                    block.Decorate(new UncorrectBlock(new NullBlock(), Color.red));
                    this.GetComponent<CoreGameManager>().UncorrectCount++;
                    break;
                case 1:
                    block.Decorate(new CorrectBlock(new NullBlock(), Color.yellow));
                    this.GetComponent<CoreGameManager>().CorrectCount++;
                    break;
            }
        }
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
            }
        }
    }
}
