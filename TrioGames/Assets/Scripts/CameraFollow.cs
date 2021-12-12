using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 10f;
    public Vector3 offset;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Change Camera size over player speed
        //float cameraSize = 10f + (desiredPosition - transform.position).magnitude;
        //mainCam.orthographicSize = cameraSize;

        // * Time.deltaTime
        transform.position = smoothedPosition;
    }
}
