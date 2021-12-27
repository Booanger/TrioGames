using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] TMP_InputField loginEmail;
    [SerializeField] TMP_InputField loginPassword;
    [SerializeField] TMP_InputField signinUsername;
    [SerializeField] TMP_InputField signinEmail;
    [SerializeField] TMP_InputField signinPassword;
    [SerializeField] TMP_Text logText;
    public static int playerLevel = 0;

    public static string currentUserPlayFabId;

    private String userEmail;
    private String userPassword;
    private String username;
    public string entityId; // Id representing the logged in player
    public string entityType; // entityType representing the logged in player

    public void OnClickLogin()
    {
        userEmail = loginEmail.text;
        userPassword = loginPassword.text;
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLogInSuccess, OnLogInFailure);
    }
    void OnLogInSuccess(LoginResult result)
    {
        logText.text = "Logging In...";

        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);


        currentUserPlayFabId = result.PlayFabId;
        GetUserData(currentUserPlayFabId);

        StartCoroutine(LoadScreen("MenuScene", 1));
        //SceneManager.LoadScene("MenuScene");
        
        //SetUserData();
        //GetUserData(result.PlayFabId);
    }
    void OnLogInFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        logText.text = "Login Error!";
    }

    public void OnClickSignIn()
    {
        username = signinUsername.text;
        userEmail = signinEmail.text;
        userPassword = signinPassword.text;
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username, DisplayName = username};
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        logText.text = "Registered successfully. You can log in.";
    }
    void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        string err = error.GenerateErrorReport();
        logText.text = "Sign In Error!\nPassword must be between 6 and 100 characters.\n" +
            "Username must be between 3 and 20 characters!";
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "BC6E3";
        }

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            loginEmail.text = PlayerPrefs.GetString("EMAIL");
            loginPassword.text = PlayerPrefs.GetString("PASSWORD");
            //OnClickLogin();
        }
    }

    void GetUserData(string myPlayFabeId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabeId,
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

    private IEnumerator LoadScreen(string name, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MenuScene");
    }
}