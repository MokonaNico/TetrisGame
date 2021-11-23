using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTetrosHandler : MonoBehaviour
{
    private Square[,] grid = new Square[4, 4];
    public Square prefab;
    
    public void UpdateNextTetros(Tetros tetros)
    {
        Clear();
        foreach (Vector2Int pos in tetros.getBlock())
        {
            grid[pos.y+1, pos.x+1].spriteRenderer.enabled = true;
            grid[pos.y+1, pos.x+1].spriteRenderer.color = tetros.GetColor();
        }
    }

    public void Clear()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                grid[y, x].spriteRenderer.enabled = false;
            }
        }
    }

    public void Awake()
    {
        InitNextTetrosZone();
    }

    private void InitNextTetrosZone()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                Square square = Instantiate(prefab);
                square.name = "next tetros : " + x + " " + y;
                square.transform.position = transform.position + new Vector3(x,y,0);
                square.spriteRenderer.enabled = false;
                grid[y, x] = square;
            }
        }
    }
}
