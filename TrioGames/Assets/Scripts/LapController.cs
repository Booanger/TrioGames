using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapController : MonoBehaviour
{
    //BoxCollider2D[] checkpoints;
    GameStatus gameStatus;
    HUDController hudController;
    LeaderboardController leaderboardController;
    void Start()
    {
        //checkpoints = GameObject.FindObjectsOfType<BoxCollider2D>();
        leaderboardController = GameObject.Find("Controller").GetComponent<LeaderboardController>();
        hudController = GameObject.Find("Controller").GetComponent<HUDController>();
        gameStatus = GameObject.Find("Controller").GetComponent<GameStatus>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag == "Finish" && true)
        {
            string carName = collision.name;
            gameStatus.IncreaseLapOfThePlayers(carName);
            if (carName == "Player")
            {
                hudController.IncreaseLapCountText();
            }
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