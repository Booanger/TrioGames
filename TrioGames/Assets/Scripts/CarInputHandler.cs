using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    // Components
    CarController carController;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.y = Input.GetAxis("Vertical");
        inputVector.x = Input.GetAxis("Horizontal");


        /*if (Input.GetKey(KeyCode.Space))
        {
            carController.driftFactor = 0.95f;
            carController.accelerationFactor = 0;
        }
        else
        {
            carController.driftFactor = 0.85f;
            carController.accelerationFactor = 30f;
        }*/

        carController.SetInputVector(inputVector);
    }
}
