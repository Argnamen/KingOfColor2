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

            block.transform.position = pos;

            block.GetComponent<Renderer>().material.SetColor("_Color", 
                new Color(
                Random.Range(0, 2),
                Random.Range(0, 2),
                Random.Range(0, 2),
                1));
        }
    }
}
