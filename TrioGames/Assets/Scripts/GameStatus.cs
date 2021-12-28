using PlayFab;
using PlayFab.ClientModels;
using System;
//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    [SerializeField] int lapCount = 3;
    private int currentLapOfThePlayer = 0;
    CarController[] allCars;
    HUDController hudController;
    int[] lapCountsOfPlayers;
    int rank;

    private void Awake()
    {
        hudController = GameObject.Find("Controller").GetComponent<HUDController>();
        rank = 0;
        allCars = FindObjectsOfType<CarController>();
        lapCountsOfPlayers = new int[allCars.Length];
        for (int i = 0; i < allCars.Length; i++) {
            lapCountsOfPlayers[i] = 0;
        }
    }
    public int GetLapCount()
    {
        return lapCount;
    }
    public int GetCurrentLap()
    {
        return currentLapOfThePlayer;
    }
    public void IncreaseCurrentLap()
    {
        currentLapOfThePlayer++;
    }
    public void IncreaseLapOfThePlayers(string carName)
    {
        for (int i = 0; i < allCars.Length; i++)
        {
            if (carName == allCars[i].name)
            {
                lapCountsOfPlayers[i]++;
            }
        }
    }
    public bool IsGameOver()
    {
        for (int i = 0; i < allCars.Length; i++) {
            if (lapCountsOfPlayers[i] == lapCount + 1) {
                lapCountsOfPlayers[i]++;
                rank++;
                if (allCars[i].name == "Player")
                {
                    hudController.StopTimer();
                    hudController.FinishedLapCountText();
                    allCars[i].gameObject.SetActive(false);
                    //PlayFabLogin.currentUserPlayFabId

                    //Highscore Table Update
                    PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
                    {
                        // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
                        Statistics = new List<StatisticUpdate> 
                        {
                            new StatisticUpdate { StatisticName = SceneManager.GetActiveScene().name, Value = (int)(hudController.GetTime() * -100)},
                        }
                    }, result => 
                    { 
                        Debug.Log("User statistics updated"); 
                    }, error => 
                    { 
                        Debug.LogError(error.GenerateErrorReport()); 
                    });

                    //To store current race's rank on player's data(title)
                    SetUserData();

                    ///TEST
                    //PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest, result =>   )
                    ///TEST

                    return true;
                }
                allCars[i].gameObject.SetActive(false);
                return false;
            }
        }
        return false;
    }

    /*
    public string FindWinner()
    {
        string winnerCarName = "";
        for (int i = 0; i < allCars.Length; i++)
        {
            if (lapCountsOfPlayers[i] > lapCount)
            {
                winnerCarName = allCars[i].name;
            }
        }
        return winnerCarName;
    }*/

    public int GetRank()
    {
        return rank;
    }
    public string GetRankString()
    {
        string rankString = rank.ToString();
        if (rank % 10 == 1) {
            rankString += "st";
        } else if (rank % 10 == 2) {
            rankString += "nd";
        } else if (rank % 10 == 3) {
            rankString += "rd";
        } else {
            rankString += "th";
        }
        return rankString;
    }


    /**
     * THIS METHOD/FUNCTION WILL BE EDITED
     */
    int GetUserData(string myPlayFabId)
    {
        int maxRace = 0;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        }, result => {
            if (result.Data != null)
            {
                maxRace = Convert.ToInt32(result.Data["maxRace"].Value);
                
            }
            /*
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("Ancestor")) 
                Debug.Log("No Ancestor");
            else 
                Debug.Log(SceneManager.GetActiveScene().name +": " + result.Data[SceneManager.GetActiveScene().name].Value);*/
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
        return maxRace;
    }

    void SetUserData()
    {
        int maxRace = GetUserData(PlayFabLogin.currentUserPlayFabId);
        if (rank < 4 && maxRace < Convert.ToInt32(SceneManager.GetActiveScene().name))
        {
            PlayFabLogin.playerLevel = Convert.ToInt32(SceneManager.GetActiveScene().name);

            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>() 
                {
                    {"maxRace", SceneManager.GetActiveScene().name}
                }

            }, result => Debug.Log("Successfully updated user data"),
            error => {
                Debug.Log("Got error setting user data Ancestor to Arthur");
                Debug.Log(error.GenerateErrorReport());
            });
        }
    }
}
