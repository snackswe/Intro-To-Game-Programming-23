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
    public BoxCollider2D hurtBox;
    public float scoreMultiplier;
    public int score;
    float airtime;

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
            if (inertiaSpeed.x > 0 && GetTriggerObject(groundCheck) == null)
            {
                inertiaSpeed.x -= Time.deltaTime * drag;
                airtime += Time.deltaTime;
                if (airtime > 0.7f)
                {
                    score += Mathf.RoundToInt(Time.deltaTime *(playerBody.velocity.x + playerBody.velocity.y) * scoreMultiplier);
                }
            } else
            {
                inertiaSpeed.x += Time.deltaTime * drag;
            }
            if ((inertiaSpeed.x < 0.4 && inertiaSpeed.x > 0) || (inertiaSpeed.x > -0.2 && inertiaSpeed.x < 0))
            {
                inertiaSpeed.x = (int)Math.Round(inertiaSpeed.x);
            }
            if (GetTriggerObject(hurtBox) != null && GetTriggerObject(hurtBox).tag == "Road")
            {
                sceneMan.YouDied();
                score = 0;
                airtime = 0;
                Debug.Log("road");
            }
        }    
    }
    private void FixedUpdate()
    {
        playerBody.velocity = new Vector2(horizontal * playerSpeed, playerBody.velocity.y);
        if (GetTriggerObject(groundCheck) != null)
        {
            if (GetTriggerObject(groundCheck).GetComponent<Rigidbody2D>() != null)
            {
                inertiaSpeed.x = GetTriggerObject(groundCheck).GetComponent<Rigidbody2D>().velocity.x;
                playerBody.velocity += inertiaSpeed;
                airtime = 0;
            }
        }
        else
        {
            playerBody.velocity += inertiaSpeed;
        }
    }
    private GameObject GetTriggerObject(Collider2D box)
    {
        Collider2D hit = Physics2D.OverlapBox(box.bounds.center, box.bounds.size, 0f, groundLayer);
        if (hit != null)
        {
            return hit.gameObject;   
        }
        return null;
    }
    private bool IsGrounded()
    {
        if (GetTriggerObject(groundCheck) != null)
        {
            return true;
        }
        return false;
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (GetTriggerObject(groundCheck) != null && GetTriggerObject(groundCheck).tag == "Ground")
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
    