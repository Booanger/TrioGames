using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] TMP_Text speedometer;
    [SerializeField] TMP_Text countdown;
    [SerializeField] TMP_Text timeCounter;

    [SerializeField] GameObject car;
    [SerializeField] int waitingTime = 3;

    private Vector3 prevPosition = Vector3.zero;
    private Vector3 deltaPosition;
    private float metersTravelledPerSecond;
    private IEnumerator coroutine;
    public float timeStart = 0f;
    bool timerActive = false;

    void Awake()
    {
        prevPosition = car.transform.position;
    }

    void Start()
    {
        timeCounter.text = timeStart.ToString("F2");
        countdown.text = waitingTime.ToString();

        coroutine = WaitForStart(waitingTime);
        StartCoroutine(coroutine);
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaPosition = car.transform.position - prevPosition;
        float distance = deltaPosition.magnitude;

        if (distance < 0.0001f)
        {
            distance = 0f;
        }

        metersTravelledPerSecond = distance / Time.deltaTime;

        speedometer.text = System.String.Format("{0:N0} mps", metersTravelledPerSecond);

        prevPosition = car.transform.position;

        if (timerActive)
        {
            timeStart += Time.deltaTime;
            timeCounter.text = timeStart.ToString("F2");
        }
    }

    private IEnumerator WaitForStart(int waitTime)
    {
        car.GetComponent<CarController>().enabled = false;
        while (waitTime != 0)
        {
            yield return new WaitForSeconds(1);
            waitTime--;
            countdown.text = waitTime.ToString();
        }
        Destroy(countdown);
        car.GetComponent<CarController>().enabled = true;
        timerActive = true;
    }
}