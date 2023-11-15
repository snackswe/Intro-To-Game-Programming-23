using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Global Variables:
    public int damage;
    public PlayerController playerController;

    //Enemy Movement Variables:
    public Transform[] patrolPoints;
    public float moveSpeed = 3;
    public int patrolDestination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.TakeDamage(damage);
        }
    }
    private void EnemyMovement()
    {
        if (patrolDestination == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[0].transform.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, patrolPoints[0].transform.position) < .2f)
            {
                patrolDestination = 1;
            }
        }
        else if (patrolDestination == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[1].transform.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, patrolPoints[1].transform.position) <= .2f)
            {
                patrolDestination = 0;
            }
        }
    }
}
