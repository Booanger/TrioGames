using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    CarController aiCar;
    int clockwise = 1;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        aiCar = GetComponent<CarController>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        float dotProduct = Vector2.Dot(transform.up, agent.steeringTarget - transform.position);


        clockwise = 1;
        Vector3 crossProduct = Vector3.Cross(transform.up, agent.steeringTarget - transform.position);
        if (crossProduct.z > 0)
        {
            clockwise = -1;
        }
            

        //Debug.DrawLine(new Vector3(0,0,0), agent.nextPosition);
        //Debug.DrawLine(new Vector3(0,0,0), crossProduct);
        Debug.DrawLine(transform.position, agent.steeringTarget);
        if (dotProduct < 1f)
        {
            clockwise = clockwise * -1;
        }
            

        aiCar.SetInputVector(new Vector2(clockwise,Vector2.Dot(transform.up, agent.steeringTarget-transform.position)));
    }
}
