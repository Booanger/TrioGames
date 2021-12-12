using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsController : MonoBehaviour
{
    [SerializeField] Transform[] points;
    private int[] destPoints;
    AiController[] aiControllers;

    // Start is called before the first frame update
    void Start()
    {
        aiControllers = FindObjectsOfType<AiController>();
        destPoints = new int[aiControllers.Length];
        for (int i = 0; i < destPoints.Length; i++)
        {
            destPoints[i] = 1;
        }
    }

    void GotoNextPoint(int indexOfCar)
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        aiControllers[indexOfCar].GetAgent().SetDestination(points[destPoints[indexOfCar]].position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0));
        //agent.SetDestination(points[destPoint].position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0));

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoints[indexOfCar] = (destPoints[indexOfCar] + 1) % points.Length;
        //destPoint = (destPoint + 1) % points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < aiControllers.Length; i++)
        {
            if (!aiControllers[i].GetAgent().pathPending && aiControllers[i].GetAgent().remainingDistance < 3f)
            {
                GotoNextPoint(i);
            }
        }

        /*
        if (!agent.pathPending && agent.remainingDistance < 3f)
        {
            GotoNextPoint();
        }*/
    }
}
