using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsController : MonoBehaviour
{
    [SerializeField] BoxCollider2D[] points;
    private int[] destPoints;
    AiController[] aiControllers;
    GameObject player;
    private int playerDestination;

    // Start is called before the first frame update
    void Start()
    {
        playerDestination = 0;
        aiControllers = FindObjectsOfType<AiController>();
        destPoints = new int[aiControllers.Length];
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < destPoints.Length; i++)
        {
            destPoints[i] = 0;
        }
    }

    void GotoNextPoint(int index)
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        aiControllers[index].GetAgent().SetDestination(points[destPoints[index]].transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0));

        //Debug.Log(points[destPoints[index]].name);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoints[index] = (destPoints[index] + 1) % points.Length;
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

    public bool IsPlayerDestinationFinal()
    {
        
        if (points[playerDestination].name == "0")
        {
            return true;
        }
        return false;
    }

    public string GetPlayerDestinationName()
    {
        //Debug.Log(points[playerDestination].name);
        return points[playerDestination].name;
    }

    public void increasePlayerDestination()
    {
        playerDestination = (playerDestination + 1) % points.Length;
    }
}
