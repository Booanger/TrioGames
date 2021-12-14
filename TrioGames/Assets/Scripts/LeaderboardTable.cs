using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    // Start is called before the first frame update
    private void Awake()
    {
        entryContainer = transform.Find("LeaderboardEntryContainer");
        entryTemplate = transform.Find("LeaderboardEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
