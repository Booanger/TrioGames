using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{

    // playerlevel verisi veritabanından gelecek
    int playerLevel;
    [SerializeField] GameObject levels;
    [SerializeField] Sprite LOCKED_LEVEL_SPRITE;
    GameObject[] allLevels;
    PlayerInfo loadedData;

    void Start()
    {
        if (PlayFabLogin.playerLevel < 5)
        {
            playerLevel = PlayFabLogin.playerLevel + 1;
        }
        else
        {
            playerLevel = PlayFabLogin.playerLevel;
        }
        
        //GetUserData();
        allLevels = new GameObject[levels.transform.childCount];
        GetAllLevels();
        LockLevels();

        //allLevels = new GameObject[levels.transform.childCount];
        //StartCoroutine(GetAllLevels());
        //StartCoroutine(LockLevels());

        /*loadedData = DataSaver.loadData<PlayerInfo>("player");
        playerLevel = GetPlayerLevel(loadedData);*/
        //GetAllLevels();
        //LockLevels();
    }

    void GetAllLevels()
    {
        //yield return new WaitForSeconds(time);
        int i = 0;
        foreach (Transform child in levels.transform)
        {
            allLevels[i] = child.gameObject;
            i += 1;
        }
    }
    void LockLevels()
    {
        //yield return new WaitForSeconds(time);
        GameObject image;
        for (int i = allLevels.Length - 1; playerLevel <= i; i--)
        {
            image = allLevels[i].transform.Find("image").gameObject;
            image.GetComponent<UnityEngine.UI.Image>().sprite = LOCKED_LEVEL_SPRITE;
            image.GetComponent<UnityEngine.UI.Button>().enabled = false;
        }
    }

    /*int GetPlayerLevel(PlayerInfo loadedData)
    {
        if (loadedData == null)
        {
            return 1;
        }
        return loadedData.level;
    }*/

    void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = PlayFabLogin.currentUserPlayFabId,
            Keys = null
        }, result => {
            if (result.Data != null)
            {
                playerLevel = Convert.ToInt32(result.Data["maxRace"].Value);
                Debug.Log(playerLevel); //databasedeki degeri dönüyor
            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
        Debug.Log(playerLevel); //ilk degeri dönüyor
    }
}