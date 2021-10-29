using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Playfield Playfield;
    public float G;
    private float nextDown;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ApplyGravityToTetros());
        Playfield.SpawnNewTetros();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Playfield.MoveTetros(new Vector2Int(-1,0));    
        } 
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Playfield.MoveTetros(new Vector2Int(1,0)); 
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

    IEnumerator  ApplyGravityToTetros()
    {
        while (true)
        {
            Playfield.ApplyGravity();
            yield return new WaitForSeconds( 1/G );
        }
    }
}
