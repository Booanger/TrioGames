using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPointsHandler : MonoBehaviour
{
    //Components
    CheckpointsController cpController;

    // Start is called before the first frame update
    void Start()
    {
        cpController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckpointsController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals(cpController.GetPlayerDestinationName()) && collision.name != "finish")//collision.name.Equals(cpController.GetPlayerDestinationName()) && collision.name != "0"
        {
            //Debug.Log(collision.name);
            cpController.increasePlayerDestination();
        }
    }
}