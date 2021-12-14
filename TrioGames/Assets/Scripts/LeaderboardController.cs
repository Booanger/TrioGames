using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Events;

public class LeaderboardController : MonoBehaviour
{
    private string secretKey = "atilgan";
    private string addScoreURL = "http://zindemedya.com/game/addscore.php";
    private string leaderboardURL = "http://zindemedya.com/game/display.php";
    string[] temp;
    void Start()
    {
        // A correct website page.
        StartCoroutine(GetLeaderboard());

        // A non-existing page.
        //StartCoroutine(GetLeaderboard());
    }

    public IEnumerator GetLeaderboard()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(leaderboardURL))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = leaderboardURL.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    temp = webRequest.downloadHandler.text.Split(',');
                    for (int i = 0; i < temp.Length; i++)
                    {
                        print(temp[i]);
                    }
                    break;
            }
        }
    }

    public IEnumerator AddScore(string name, int score)
    {
        string hash = Md5Sum(name + score + secretKey);
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);
        form.AddField("hash", hash);

        using (UnityWebRequest www = UnityWebRequest.Post(addScoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);
        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);
        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";
        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }
        return hashString.PadLeft(32, '0');
    }
}