using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Global Variables:

    public float playerSpeed = 1f;
    public Rigidbody2D playerBody;
    public float jumpForce = 10;
    public float drag = 1;
    public Vector2 inertiaSpeed;
    public SceneLogicHandler sceneMan;
    private float horizontal;
    public LayerMask groundLayer;
    public BoxCollider2D groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        //groundCheck = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneLogicHandler.isPaused)
        {
            Jump();
            horizontal = Input.GetAxisRaw("Horizontal");
            if (inertiaSpeed.x > 0)
            {
                inertiaSpeed.x -= Time.deltaTime * drag;
            } else
            {
                inertiaSpeed.x += Time.deltaTime * drag;
            }
            if ((inertiaSpeed.x < 0.4 && inertiaSpeed.x > 0) || (inertiaSpeed.x > -0.2 && inertiaSpeed.x < 0))
            {
                inertiaSpeed.x = (int)Math.Round(inertiaSpeed.x);
            }
            if (GetGroundObject() != null && GetGroundObject().tag == "Road")
            {
                sceneMan.YouDied();
                Debug.Log("road");
            }
        }    
    }
    private void FixedUpdate()
    {
        playerBody.velocity = new Vector2(horizontal * playerSpeed, playerBody.velocity.y);
        if (GetGroundObject() != null)
        {
            if (GetGroundObject().GetComponent<Rigidbody2D>() != null)
            {
                inertiaSpeed.x = GetGroundObject().GetComponent<Rigidbody2D>().velocity.x;
                playerBody.velocity += inertiaSpeed;
            }
        }
        else
        {
            playerBody.velocity += inertiaSpeed;
        }
    }
    private GameObject GetGroundObject()
    {
        
        Collider2D hit = Physics2D.OverlapBox(groundCheck.bounds.center, groundCheck.bounds.size, 0f, groundLayer);
        if (hit != null)
        {
            return hit.gameObject;   
        }
        return null;
    }
    private bool IsGrounded()
    {
        if (GetGroundObject() != null)
        {
            return true;
        }
        return false;
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (GetGroundObject() != null && GetGroundObject().tag == "Ground")
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
            }
        }

        if (Input.GetButtonUp("Jump") && playerBody.velocity.y > 0f)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, playerBody.velocity.y * 0.2f);
        }
    }
}
    