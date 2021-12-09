using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] GameObject playerCar;
    private const float ZERO_SPEED_ANGLE = 187f;
    private const float MAX_SPEED_ANGLE = -83f;
    private float totalAngleSize = Mathf.Abs(ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE);
    private float angleVersusSpeedRatio;
    private Transform needleTransform;

    // Car variables
    private float maxCarSpeed;
    private float currentCarSpeed;

    private void Awake()
    {
        maxCarSpeed = playerCar.GetComponent<CarController>().maxSpeed;
        angleVersusSpeedRatio = totalAngleSize / maxCarSpeed;
        needleTransform = transform.Find("IMG_Needle");
        needleTransform.eulerAngles = new Vector3(0, 0, ZERO_SPEED_ANGLE);
    }

    private void FixedUpdate()
    {
        currentCarSpeed = playerCar.GetComponent<CarController>().GetVelocityVersusUp();
        needleTransform.eulerAngles = new Vector3(0, 0, SetNeedle());
    }

    private float SetNeedle()
    {
        return ZERO_SPEED_ANGLE - Mathf.Abs(angleVersusSpeedRatio * currentCarSpeed);
    }
}