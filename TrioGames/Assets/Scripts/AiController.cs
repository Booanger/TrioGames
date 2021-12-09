using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    CarController aiCar;


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
        Debug.Log(agent.steeringTarget.magnitude);
        Debug.DrawLine(transform.position,agent.steeringTarget);
        //aiCar.SetInputVector();
    }
}
