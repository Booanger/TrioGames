using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class LeaderboardController : MonoBehaviour
{
    public GameObject leaderboardPanel;
    public GameObject listingPrefab;
    public Transform listingContainer;

    public void GetLeaderboarder()
    {
        var requestLeaderboard = new GetLeaderboardRequest
        {
            StartPosition = 0,
            //StatisticName = "race1",
            StatisticName = SceneManager.GetActiveScene().name,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeaderboard, OnErrorLeaderboard);
    }

    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        leaderboardPanel.SetActive(true);
        int rank = 1;
        foreach(PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject tempListing = Instantiate(listingPrefab, listingContainer);
            LeaderboardListing LL = tempListing.GetComponent<LeaderboardListing>();
            LL.playerPosText.text = rank.ToString();
            LL.playerNameText.text = player.DisplayName;
            LL.playerScoreText.text = (((float)player.StatValue) / -100).ToString() + " sec";
            //LL.playerScoreText.text = player.StatValue.ToString();
            rank++;
        }
    }
    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}
