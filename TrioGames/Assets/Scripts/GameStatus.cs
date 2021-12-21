using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField] int lapCount = 3;
    private int currentLapOfThePlayer = 0;
    CarController[] allCars;
    int[] lapCountsOfPlayers;
    int rank;

    private void Awake()
    {
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
                    allCars[i].gameObject.SetActive(false);
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

    public int getRank()
    {
        return rank;
    }
}
