using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMover : MonoBehaviour
{
    // Global Variables:
    public float baseMoveForce = 10f;
    public Vector2 massCenter;
    public bool flipped = false;
    public bool inMotion = false;  
    private float dir = -1;
    public float speedTolerance;
    public float varianceFrequency;
    float newSpeed;
    Rigidbody2D cabinRb;
    
    // Start is called before the first frame update
    void Start()
    {            
        cabinRb = GetComponent<Rigidbody2D>();
        InvokeRepeating("ChangeSpeed", 0, varianceFrequency);
        if (flipped)
        {
            dir = 1;
            massCenter.x *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cabinRb.centerOfMass = massCenter;
    }
    private void FixedUpdate()
    {
        if (inMotion && !SceneLogicHandler.isPaused) 
        { 
            cabinRb.velocity = new Vector2(dir * newSpeed, cabinRb.velocity.y); 
        }
    }
    private void ChangeSpeed()
    {
        newSpeed = Random.Range(baseMoveForce -speedTolerance, baseMoveForce +speedTolerance);
    }
}
