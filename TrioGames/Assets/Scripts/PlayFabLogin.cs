using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] TMP_InputField loginUsername;
    [SerializeField] TMP_InputField loginEmail;
    [SerializeField] TMP_InputField loginPassword;
    [SerializeField] TMP_InputField signinUsername;
    [SerializeField] TMP_InputField signinEmail;
    [SerializeField] TMP_InputField signinPassword;
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
        entityId = result.EntityToken.Entity.Id;
        entityType = result.EntityToken.Entity.Type;
        SceneManager.LoadScene("MenuScene");
    }
    void OnLogInFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void OnClickSignIn()
    {
        username = signinUsername.text;
        userEmail = signinEmail.text;
        userPassword = signinPassword.text;
        // If password or email fields are not empty
        if (!(string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(username)))
        {
            var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username};
            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
        }
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Registered");
    }
    void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "BC6E3";
        }
    }
}