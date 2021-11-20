using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Playfield : MonoBehaviour
{
    private static int w = 10, h = 24;
    private Square[,] grid = new Square[h, w];
    public Square prefab;
    private Tetros currentTetros;

    private void Awake()
    {
        InitPlayField();
    }

    private void InitPlayField()
    {
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                Square square = Instantiate(prefab);
                square.name = x + " " + y;
                square.transform.position = new Vector3(x,y,0);
                square.spriteRenderer.enabled = false;
                grid[y, x] = square;
            }
        }
    }
    
    public void SpawnNewTetros()
    {
        int r = Random.Range(0, 7);
        if(r == 0) currentTetros = new IShape();
        else if(r == 1) currentTetros = new OShape();
        else if(r == 2) currentTetros = new TShape();
        else if(r == 3) currentTetros = new SShape();
        else if(r == 4) currentTetros = new ZShape();
        else if(r == 5) currentTetros = new JShape();
        else if(r == 6) currentTetros = new LShape();

        currentTetros.pos = new Vector2Int(4,21);
        SetAllBlocksOfTetros(true);
    }
    
    public void MoveTetros(Vector2Int dir)
    {
        if(currentTetros == null) return;

        SetAllBlocksOfTetros(false);

        currentTetros.pos += dir;
        if (IsOutOfBound() || IsTouchingStaticBlock())
            currentTetros.pos -= dir;

        SetAllBlocksOfTetros(true);
    }

    public void ApplyGravity()
    {
        if(currentTetros == null) return;
        
        SetAllBlocksOfTetros(false);
        
        currentTetros.pos += new Vector2Int(0,-1);
        
        if (IsTouchingTheGround() || IsTouchingStaticBlock())
        {
            currentTetros.pos -= new Vector2Int(0,-1);
            SetAllBlocksOfTetros(true);
            CheckLine();
            SpawnNewTetros();
            return;
        }
        
        SetAllBlocksOfTetros(true);
        
    }

    public bool IsOutOfBound()
    {
        Vector2Int[] blocks = currentTetros.getBlock();
        foreach (Vector2Int b in blocks)
        {
            if (b.x < 0 || b.x >= 10)
                return true;
        }
        return false;
    }

    public bool IsTouchingStaticBlock()
    {
        Vector2Int[] blocks = currentTetros.getBlock();
        foreach (Vector2Int b in blocks)
        {
            if (grid[b.y, b.x].spriteRenderer.enabled)
                return true;
        }
        return false;
    }

    public bool IsTouchingTheGround()
    {
        Vector2Int[] blocks = currentTetros.getBlock();
        foreach (Vector2Int b in blocks)
        {
            if (b.y < 0)
                return true;
        }
        return false;
    }

    private void SetAllBlocksOfTetros(bool value)
    {
        Vector2Int[] blocks = currentTetros.getBlock();
        foreach (Vector2Int b in blocks)
        {
            grid[b.y, b.x].spriteRenderer.enabled = value;
            grid[b.y, b.x].spriteRenderer.color = currentTetros.GetColor();
        }
    }

    public void RotateTetros(bool isLeft)
    {
        SetAllBlocksOfTetros(false);
        currentTetros.Rotate(isLeft);
        if(IsOutOfBound() || IsTouchingStaticBlock()) currentTetros.Rotate(!isLeft);
        SetAllBlocksOfTetros(true);
    }

    public void CheckLine()
    {
        int line = -1;
        for (int y = 0; y < h; y++)
        {
            int count = 0;
            for (int x = 0; x < w; x++)
            {
                if (grid[y, x].spriteRenderer.enabled) count++;
                else break;
            }

            if (count == 10)
            {
                line = y;
                break;
            }
        }

        if(line == -1) return;

        for (int i = 0; i < w; i++)
        {
            grid[line, i].spriteRenderer.enabled = false;
        }
        
        for (int y = line + 1; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                grid[y-1, x].spriteRenderer.enabled = grid[y, x].spriteRenderer.enabled;
                grid[y-1, x].spriteRenderer.color = grid[y, x].spriteRenderer.color;
            }
        }
        
        CheckLine();
    }
}
