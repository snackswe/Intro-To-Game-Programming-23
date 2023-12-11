using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTriggers : MonoBehaviour
{
    // Global Variables:
    public GameObject[] Cabins;
    public bool activateTrucksOnTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (activateTrucksOnTrigger)
            {
                foreach (GameObject truck in Cabins)
                {
                    TruckMover mover = truck.GetComponent<TruckMover>();
                    mover.inMotion = true;
                }    
            }
            else
            {
                foreach (GameObject truck in Cabins)
                {
                    TruckMover mover = truck.GetComponent<TruckMover>();
                    mover.inMotion = false;
                }    
            }
        }

    }
}
