using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapController : MonoBehaviour
{
    //BoxCollider2D[] checkpoints;
    GameStatus gameStatus;
    HUDController hudController;
    LeaderboardController leaderboardController;
    CheckpointsController cpController;
    void Start()
    {
        //checkpoints = GameObject.FindObjectsOfType<BoxCollider2D>();
        leaderboardController = GameObject.Find("Controller").GetComponent<LeaderboardController>();
        hudController = GameObject.Find("Controller").GetComponent<HUDController>();
        gameStatus = GameObject.Find("Controller").GetComponent<GameStatus>();
        cpController = GameObject.Find("Controller").GetComponent<CheckpointsController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Finish")
        {
            string carName = collision.name;
            
            //TEST
            if (carName.Equals("Player") && cpController.GetPlayerDestinationName() == "finish")
            {
                //Debug.Log(gameObject.name);
                hudController.IncreaseLapCountText();
                gameStatus.IncreaseLapOfThePlayers(carName);
                cpController.increasePlayerDestination();
            }
            else if (carName != "Player")
            {
                gameStatus.IncreaseLapOfThePlayers(carName);
            }

            //gameStatus.IncreaseLapOfThePlayers(carName);
            //if (carName == "Player")
            //{
                //hudController.IncreaseLapCountText();
            //}
            if (gameStatus.IsGameOver())
            {
                //string winner = gameStatus.FindWinner();

                int rank = gameStatus.GetRank();
                hudController.SetRankText(gameStatus.GetRankString());
                //transform.gameObject.active = false;
                Debug.Log("Game Over In " + hudController.PrintTimer() + " seconds. Rank: " + rank);
                StartCoroutine(WaitForLeaderboard());
            }
        }
    }

    IEnumerator WaitForLeaderboard()
    {
        yield return new WaitForSeconds(3);
        leaderboardController.GetLeaderboarder();
    }
}