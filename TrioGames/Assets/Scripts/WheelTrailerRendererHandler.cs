using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailerRendererHandler : MonoBehaviour
{
    // Components
    CarController carController;
    TrailRenderer trailRenderer;
    private void Awake()
    {
        // Get the car controller
        carController = GetComponentInParent<CarController>();

        // Get the trail renderer component
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
            trailRenderer.emitting = true;
        else trailRenderer.emitting = false;
        
    }
}
