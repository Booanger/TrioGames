using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    // userlevel verisi veritabanndan gelecek
    int userLevel = 3;

    int i = 0;
    [SerializeField] GameObject levels;
    [SerializeField] Sprite LOCKED_LEVEL_SPRITE;
    GameObject[] allLevels;
    void Awake()
    {
        allLevels = new GameObject[levels.transform.childCount];
        GetAllLevels();
        LockLevels();
    }
    void GetAllLevels()
    {
        foreach (Transform child in levels.transform)
        {
            allLevels[i] = child.gameObject;
            i += 1;
        }
    }
    void LockLevels()
    {
        GameObject image;
        for (int i = allLevels.Length - 1; userLevel <= i; i--)
        {
            image = allLevels[i].transform.Find("image").gameObject;
            image.GetComponent<UnityEngine.UI.Image>().sprite = LOCKED_LEVEL_SPRITE;
        }
    }
}