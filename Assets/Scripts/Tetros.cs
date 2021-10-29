using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tetros
{
    public enum Direction
    {
        down, left, up, right
    }
    
    public Vector2Int pos = new Vector2Int();
    public Direction dir = Direction.down;

    public abstract Color GetColor();

    public abstract Vector2Int[] getBlock();

    public void Rotate(bool left)
    {
        if (left)
        {
            if (dir == Direction.down) dir = Direction.right;
            else if (dir == Direction.right) dir = Direction.up;
            else if (dir == Direction.up) dir = Direction.left;
            else if (dir == Direction.left) dir = Direction.down;  
        }
        else
        {
            if (dir == Direction.down) dir = Direction.left;
            else if (dir == Direction.left) dir = Direction.up;
            else if (dir == Direction.up) dir = Direction.right;
            else if (dir == Direction.right) dir = Direction.down;
        }
        
    }
    
}



public class IShape : Tetros
{
    public override Vector2Int[] getBlock()
    {
        Vector2Int[] list = new Vector2Int[4];
        if (dir == Direction.down || dir == Direction.up)
        {
            list[0] = pos + new Vector2Int(-1,0);
            list[1] = pos + new Vector2Int(0,0);
            list[2] = pos + new Vector2Int(1,0);
            list[3] = pos + new Vector2Int(2,0);
        }
        else if (dir == Direction.left || dir == Direction.right)
        {
            list[0] = pos + new Vector2Int(0,-1);
            list[1] = pos + new Vector2Int(0,0);
            list[2] = pos + new Vector2Int(0,1);
            list[3] = pos + new Vector2Int(0,2);
        }

        return list;
    }

    public override Color GetColor()
    {
        return Color.cyan;
    }
}

public class OShape : Tetros
{
    public override Vector2Int[] getBlock()
    {
        Vector2Int[] list = new Vector2Int[4];
        list[0] = pos + new Vector2Int(0,0);
        list[1] = pos + new Vector2Int(1,0);
        list[2] = pos + new Vector2Int(0,1);
        list[3] = pos + new Vector2Int(1,1);
        return list;
    }
    
    public override Color GetColor()
    {
        return Color.yellow;
    }
}

public class TShape : Tetros
{
    public override Vector2Int[] getBlock()
    {
        Vector2Int[] list = new Vector2Int[4];
        if (dir == Direction.down)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,0);
            list[2] = pos + new Vector2Int(1,0);
            list[3] = pos + new Vector2Int(0,-1);
        }
        else if (dir == Direction.left)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,0);
            list[2] = pos + new Vector2Int(0,-1);
            list[3] = pos + new Vector2Int(0,1);
        }
        else if (dir == Direction.up)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,0);
            list[2] = pos + new Vector2Int(1,0);
            list[3] = pos + new Vector2Int(0,1);
        }
        else if (dir == Direction.right)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(1,0);
            list[2] = pos + new Vector2Int(0,1);
            list[3] = pos + new Vector2Int(0,-1);
        }
        return list;
    }
    
    public override Color GetColor()
    {
        return new Color(111,0,255);
    }
}

public class SShape : Tetros
{
    public override Vector2Int[] getBlock()
    {
        Vector2Int[] list = new Vector2Int[4];
        if (dir == Direction.down)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,0);
            list[2] = pos + new Vector2Int(0,1);
            list[3] = pos + new Vector2Int(1,1);
        }
        else if (dir == Direction.left)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(0,1);
            list[2] = pos + new Vector2Int(1,0);
            list[3] = pos + new Vector2Int(1,-1);
        }
        else if (dir == Direction.up)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(1,0);
            list[2] = pos + new Vector2Int(0,-1);
            list[3] = pos + new Vector2Int(-1,-1);
        }
        else if (dir == Direction.right)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,0);
            list[2] = pos + new Vector2Int(-1,1);
            list[3] = pos + new Vector2Int(0,-1);
        }
        return list;
    }
    
    public override Color GetColor()
    {
        return Color.green;
    }
}

public class ZShape : Tetros
{
    public override Vector2Int[] getBlock()
    {
        Vector2Int[] list = new Vector2Int[4];
        if (dir == Direction.down)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,1);
            list[2] = pos + new Vector2Int(0,1);
            list[3] = pos + new Vector2Int(1,0);
        }
        else if (dir == Direction.left)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(1,0);
            list[2] = pos + new Vector2Int(0,-1);
            list[3] = pos + new Vector2Int(1,1);
        }
        else if (dir == Direction.up)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,0);
            list[2] = pos + new Vector2Int(0,-1);
            list[3] = pos + new Vector2Int(1,-1);
        }
        else if (dir == Direction.right)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,0);
            list[2] = pos + new Vector2Int(0,1);
            list[3] = pos + new Vector2Int(-1,-1);
        }
        return list;
    }
    
    public override Color GetColor()
    {
        return Color.red;
    }
}

public class JShape : Tetros
{
    public override Vector2Int[] getBlock()
    {
        Vector2Int[] list = new Vector2Int[4];
        if (dir == Direction.down)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,0);
            list[2] = pos + new Vector2Int(1,0);
            list[3] = pos + new Vector2Int(1,-1);
        }
        else if (dir == Direction.left)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(0,1);
            list[2] = pos + new Vector2Int(0,-1);
            list[3] = pos + new Vector2Int(-1,-1);
        }
        else if (dir == Direction.up)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(1,0);
            list[2] = pos + new Vector2Int(-1,0);
            list[3] = pos + new Vector2Int(-1,1);
        }
        else if (dir == Direction.right)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(0,-1);
            list[2] = pos + new Vector2Int(0,1);
            list[3] = pos + new Vector2Int(1,1);
        }
        return list;
    }
    
    public override Color GetColor()
    {
        return Color.blue;
    }
}

public class LShape : Tetros
{
    public override Vector2Int[] getBlock()
    {
        Vector2Int[] list = new Vector2Int[4];
        if (dir == Direction.down)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(-1,0);
            list[2] = pos + new Vector2Int(1,0);
            list[3] = pos + new Vector2Int(-1,-1);
        }
        else if (dir == Direction.left)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(0,1);
            list[2] = pos + new Vector2Int(0,-1);
            list[3] = pos + new Vector2Int(-1,1);
        }
        else if (dir == Direction.up)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(1,0);
            list[2] = pos + new Vector2Int(-1,0);
            list[3] = pos + new Vector2Int(1,1);
        }
        else if (dir == Direction.right)
        {
            list[0] = pos + new Vector2Int(0,0);
            list[1] = pos + new Vector2Int(0,-1);
            list[2] = pos + new Vector2Int(0,1);
            list[3] = pos + new Vector2Int(1,-1);
        }
        return list;
    }
    
    public override Color GetColor()
    {
        return new Color(255,150,0);
    }
}