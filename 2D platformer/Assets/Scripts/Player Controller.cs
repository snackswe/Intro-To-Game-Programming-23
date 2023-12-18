using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI airBonusText;
    public Animator animator;
    public UnityEngine.UI.Slider AirMeter;
    public float scoreMultiplier;
    public static int score;
    private bool isFacingRight = true;
    float airtime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneLogicHandler.isPaused)
        {
            Flip();
            Jump();
            if (airtime > 1.5f && airtime > AirMeter.value)
            {
                AirMeter.value += Time.deltaTime;
            }
            if (AirMeter.value > airtime)
            {
                AirMeter.value -= Time.deltaTime * 0.5f;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                animator.SetBool("Running", true);
            }else
            {
                animator.SetBool("Running", false);
            }
        }

    }

    private void FixedUpdate()
    {
        if (!SceneLogicHandler.isPaused)
        {
            Jump();
            horizontal = Input.GetAxisRaw("Horizontal");
            if ((inertiaSpeed.x > 0 || inertiaSpeed.x < 0) && GetTriggerObject(groundCheck) == null)
            {
                airtime += Time.deltaTime;
                animator.SetBool("Grounded", false);

                if (inertiaSpeed.x > 0)
                {
                    inertiaSpeed.x -= Time.deltaTime * drag;
                } else
                {
                    inertiaSpeed.x += Time.deltaTime * drag;
                }
                if (airtime > 1.5f)
                {
                    float airBonus = airtime + scoreMultiplier;
                    score += Mathf.RoundToInt(Time.deltaTime * playerBody.velocity.x * airBonus);
                    airBonusText.text = "x " + airtime.ToString("F2");
                }
                if (airtime > 4)
                {
                    airtime = 4;
                }
            }
            if (GetTriggerObject(hurtBox) != null && GetTriggerObject(hurtBox).tag == "Road")
            {
                sceneMan.YouDied();
                score = 0;
                airtime = 0;
                Debug.Log("road");
            }
            playerBody.velocity = new Vector2(horizontal * playerSpeed, playerBody.velocity.y);

            if (GetTriggerObject(groundCheck) != null)
            {
                animator.SetBool("Grounded", true);
                if (GetTriggerObject(groundCheck).GetComponent<Rigidbody2D>() != null)
                {
                    inertiaSpeed.x = GetTriggerObject(groundCheck).GetComponent<Rigidbody2D>().velocity.x;
                    playerBody.velocity += inertiaSpeed;
                    airtime = 0;
                    airBonusText.text = "";
                }
            }
            else
            {
                playerBody.velocity += inertiaSpeed;
            }  
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
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            transform.localScale = localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
    