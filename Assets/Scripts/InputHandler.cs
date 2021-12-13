using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Playfield Playfield;
    private float nextDown;
    
    private bool isPushingLeft, isPushingRight;
    private double leftTimer, rightTimer;

    public double MovingPauseTime = 0.5;
    public double movingRate = 0.2;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Playfield.MoveTetros(new Vector2Int(-1,0));
            isPushingLeft = true;
            leftTimer = Time.time + MovingPauseTime;
        } 
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Playfield.MoveTetros(new Vector2Int(1,0));
            isPushingRight = true;
            rightTimer = Time.time + MovingPauseTime;
        }

        if (isPushingLeft && Time.time > leftTimer)
        {
            Playfield.MoveTetros(new Vector2Int(-1,0));
            leftTimer = Time.time + movingRate;
        }
        
        if (isPushingRight && Time.time > rightTimer)
        {
            Playfield.MoveTetros(new Vector2Int(1,0));
            rightTimer = Time.time + movingRate;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isPushingLeft = false;
        } 
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isPushingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Playfield.RotateTetros(true);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Playfield.RotateTetros(false);
        }

        if (Input.GetKey(KeyCode.DownArrow) && Time.time > nextDown)
        {
            nextDown = Time.time + 0.1f;
            Playfield.ApplyGravity();
        }
    }
}
