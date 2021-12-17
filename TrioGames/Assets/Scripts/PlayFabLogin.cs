using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] TMP_InputField loginEmail;
    [SerializeField] TMP_InputField loginPassword;
    [SerializeField] TMP_InputField signinUsername;
    [SerializeField] TMP_InputField signinEmail;
    [SerializeField] TMP_InputField signinPassword;
    [SerializeField] TMP_Text logText;

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
        entityId = result.EntityToken.Entity.Id;
        entityType = result.EntityToken.Entity.Type;

        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);

        SceneManager.LoadScene("MenuScene");
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
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username};
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
        }
    }
}