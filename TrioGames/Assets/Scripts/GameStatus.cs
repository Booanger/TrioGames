using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField] int lapCount = 3;
    private int currentLapOfThePlayer = 0;
    CarController[] allCars;
    int[] lapCountsOfPlayers;

    private void Awake()
    {
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
            if (lapCountsOfPlayers[i] > lapCount) {
                return true;
            }
        }
        return false;
    }
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
    }
}
