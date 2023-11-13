using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Global Variables:

    public float playerSpeed = 0.05f;
    public Rigidbody2D playerBody;
    public float jumpForce = 500;
    public bool isJumping = false;
    public HealthBar healthBar;
    public int maxHealth = 20;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
    }
    private void MovePlayer()
    {
        Vector3 newPos = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("A");
            newPos.x -= playerSpeed;
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("D");
            newPos.x += playerSpeed;
        }
        transform.position = newPos;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            //Debug.Log("Jump");
            playerBody.AddForce(new Vector3(playerBody.velocity.x, jumpForce, 0));
            isJumping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping=false;
        }
        if (collision.gameObject.tag == "Fire")
        {
            TakeDamage(5);
        }
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage() called. Damage = " + damage);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
