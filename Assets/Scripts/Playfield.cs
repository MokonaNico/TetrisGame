using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Playfield : MonoBehaviour
{
    private static int w = 10, h = 24;
    private Square[,] grid = new Square[h, w];
    public Square prefab;
    private Tetros currentTetros;
    private Tetros nextTetros;
    private List<Tetros> nextTetrosList = new List<Tetros>();
    private int score = 0;
    public float G = 1;
    private static Random random = new Random();
    private bool gameIsOn = true;

    public UIHandler uiHandler;
    public NextTetrosHandler _NextTetrosHandler;
    public BestScoreHandler BestScoreHandler;

    private IEnumerator gravityCoroutine;
    

    void Start()
    {
        gameIsOn = true;
        gravityCoroutine = ApplyGravityToTetros();
        StartCoroutine(gravityCoroutine);
        StartCoroutine(IncreaseDifficulty());
        SpawnNewTetros();
        uiHandler.ShowGameOver(false);
    }
    
    IEnumerator ApplyGravityToTetros()
    {
        while (true)
        {
            ApplyGravity();
            yield return new WaitForSeconds( 1/G );
        }
    }
    
    
    private IEnumerator IncreaseDifficulty()
    {
        G -= 1f;
        while (true)
        {
            G += 1f;
            yield return new WaitForSeconds( 60 );
        }
    }
    
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
        if (nextTetros == null) nextTetros = GetNextTetrosToSpawn();
        currentTetros = nextTetros;
        nextTetros = GetNextTetrosToSpawn();
        _NextTetrosHandler.UpdateNextTetros(nextTetros);
        
        Vector2Int spawnPosition = new Vector2Int(4,21);
        currentTetros.pos = spawnPosition;
        if (IsTouchingStaticBlock())
        {
            StopCoroutine(gravityCoroutine);
            gameIsOn = false;
            uiHandler.ShowGameOver(true);
            BestScoreHandler.AddNewScore(score);
            BestScoreHandler.UpdateScore();
        }
        else
        {
            SetAllBlocksOfTetros(true);
        }
        
    }
    
    private Tetros GetNextTetrosToSpawn()
    {
        if (nextTetrosList.Count == 0)
        {
            List<Tetros> list = Tetros.GetListOfAllTetros();
            for (int i = 0; i < 7; i++)
            {
                int removeIndex = random.Next(list.Count);
                nextTetrosList.Add(list[removeIndex]);
                list.RemoveAt(removeIndex);
            }
        }
        Tetros tetros = nextTetrosList[0];
        nextTetrosList.RemoveAt(0);
        return tetros;
    }
    
    public void MoveTetros(Vector2Int dir)
    {
        if(currentTetros == null || !gameIsOn) return;

        SetAllBlocksOfTetros(false);
        currentTetros.pos += dir;
        if (IsOutOfBound() || IsTouchingStaticBlock())
            currentTetros.pos -= dir;
        SetAllBlocksOfTetros(true);
    }

    public void ApplyGravity()
    {
        if(currentTetros == null || !gameIsOn) return;
        
        SetAllBlocksOfTetros(false);
        currentTetros.pos += new Vector2Int(0,-1);
        if (IsTouchingTheGround() || IsTouchingStaticBlock())
        {
            currentTetros.pos -= new Vector2Int(0,-1);
            SetAllBlocksOfTetros(true);
            int nbrLine = CheckLine();
            switch (nbrLine)
            {
                case 1:
                    score += 100;
                    break; 
                case 2:
                    score += 300;
                    break;
                case 3:
                    score += 500;
                    break;
                case 4:
                    score += 800;
                    break;
            }
            uiHandler.UpdateScore(score);
            
            
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


    public int CheckLine()
    {
        int line = -1;
        
        // Look up for lines to remove
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

        if(line == -1) return 0; // If there is no line to remove, just return
        
        // Remove the line
        for (int i = 0; i < w; i++)
        {
            grid[line, i].spriteRenderer.enabled = false;
        }

        // Move all the line above, one line to the bottom
        for (int y = line + 1; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                grid[y-1, x].spriteRenderer.enabled = grid[y, x].spriteRenderer.enabled;
                grid[y-1, x].spriteRenderer.color = grid[y, x].spriteRenderer.color;
            }
        }
        
        return 1 + CheckLine();
    }
}
