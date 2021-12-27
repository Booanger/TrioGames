using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{

    // playerlevel verisi veritabanından gelecek
    int playerLevel = 3;
    [SerializeField] GameObject levels;
    [SerializeField] Sprite LOCKED_LEVEL_SPRITE;
    GameObject[] allLevels;
    PlayerInfo loadedData;

    void Awake()
    {
        allLevels = new GameObject[levels.transform.childCount];
        /*loadedData = DataSaver.loadData<PlayerInfo>("player");
        playerLevel = GetPlayerLevel(loadedData);*/
        GetAllLevels();
        LockLevels();
    }
    void GetAllLevels()
    {
        int i = 0;
        foreach (Transform child in levels.transform)
        {
            allLevels[i] = child.gameObject;
            i += 1;
        }
    }
    void LockLevels()
    {
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
}