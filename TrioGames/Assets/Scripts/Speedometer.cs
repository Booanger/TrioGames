using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] GameObject playerCar;
    private const float ZERO_SPEED_ANGLE = 187f;
    private const float MAX_SPEED_ANGLE = -83f;
    private float totalAngleSize = Mathf.Abs(ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE);
    private float angleVersusSpeedRatio;
    private Transform needleTransform;
    private Transform speedLabelTemplateTransform;

    // Car variables
    private float maxCarSpeed;
    private float currentCarSpeed;

    private void Awake()
    {
        maxCarSpeed = playerCar.GetComponent<CarController>().maxSpeed;
        angleVersusSpeedRatio = totalAngleSize / maxCarSpeed;
        needleTransform = transform.Find("IMG_Needle");
        speedLabelTemplateTransform = transform.Find("speedLabelTemplate");
        speedLabelTemplateTransform.gameObject.SetActive(false);
        needleTransform.eulerAngles = new Vector3(0, 0, ZERO_SPEED_ANGLE);

        CreateSpeedLabels();
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

    private void CreateSpeedLabels()
    {
        int labelAmount = 10;

        for (int i = 0; i < labelAmount; i++)
        {
            Transform speedLabelTransform = Instantiate(speedLabelTemplateTransform, transform);
            float currentLabelOverLabelAmount = (float) i / (labelAmount - 1);
            float speedLabelAngle = ZERO_SPEED_ANGLE - totalAngleSize * currentLabelOverLabelAmount;
            speedLabelTransform.eulerAngles = new Vector3(0, 0, speedLabelAngle);
            speedLabelTransform.Find("speedText").GetComponent<TMP_Text>().text = Mathf.RoundToInt(currentLabelOverLabelAmount * maxCarSpeed).ToString();
            speedLabelTransform.Find("speedText").eulerAngles = Vector3.zero;
            speedLabelTransform.gameObject.SetActive(true);
        }
        needleTransform.SetAsLastSibling();
    }
}