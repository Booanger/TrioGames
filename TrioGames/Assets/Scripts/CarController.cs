using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.87f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 25;


    // Local variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;
    float velocityVersusUp = 0;

    // Components
    Rigidbody2D carRigidbody2D;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update.
    void Start()
    {

    }

    // Update is called once per frame.
    void Update()
    {
        
    }

    // Frame-rate independent for physics calculations.
    private void FixedUpdate()
    {
        ApplyEngineForce();

        StopOrthogonalVelocity();

        ApplySteering();

        //Debug.Log(velocityVersusUp);
    }

    private void ApplyEngineForce()
    {
        // Calculate how much "forward" we are going in terms of the direction of our velocity
        velocityVersusUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        // Limit so we cannot go faster than max speed in the "forward" direction
        if (velocityVersusUp > maxSpeed && accelerationInput > 0)
            return;

        // Limit so we cannot go faster than %50 of max speed in the "reverse" direction
        if (velocityVersusUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        // Limit so we cannot go faster in any direction while accelerating
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        // Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 1.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

        // Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        // Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        //Limit the car ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        // Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        // Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    private void StopOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        // Return how fast the car is moving sideways
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBreaking)
    {
        lateralVelocity = GetLateralVelocity();
        isBreaking = false;

        // Check if the car is moving forward and the brakes are applied
        if (accelerationInput < 0 && velocityVersusUp > 0)
        {
            isBreaking = true;
            return true;
        }

        // If the car has a lot of side movement then the tires should be screeching
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
        {
            return true;
        }

        return false;
    }
    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        if (Vector2.Dot(transform.up, carRigidbody2D.velocity) < 0)
            steeringInput *= -1;

        accelerationInput = inputVector.y;
    }
    public float GetVelocityVersusUp()
    {
        return velocityVersusUp;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }
}