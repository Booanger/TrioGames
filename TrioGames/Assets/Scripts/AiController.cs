using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{

    [SerializeField] Transform target;
    NavMeshAgent agent;
    CarController aiCar;
    int clockwise;
    int gas;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        aiCar = GetComponent<CarController>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    /*
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.SetDestination(points[destPoint].position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0));

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }*/

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!agent.pathPending && agent.remainingDistance < 3f)
        {
            GotoNextPoint();
        }*/

        //agent.SetDestination(target.position);
        //float dotProduct = Vector2.Dot(transform.up, agent.steeringTarget - transform.position);


        clockwise = 1;
        gas = 1;
        Vector3 crossProduct = Vector3.Cross(transform.up, agent.steeringTarget - transform.position);
        if (crossProduct.z > 0f)
        {
            clockwise = -1;
            //gas = -1;
        }
            
        //Debug.DrawLine(new Vector3(0,0,0), agent.nextPosition);
        //Debug.DrawLine(new Vector3(0,0,0), crossProduct);
        Debug.DrawLine(transform.position, agent.steeringTarget);
        if (gas < 0)
            clockwise = clockwise * -1;
            

        aiCar.SetInputVector(new Vector2(clockwise, gas)); //aiCar.SetInputVector(new Vector2(clockwise, dotProduct)); 
    }
}
