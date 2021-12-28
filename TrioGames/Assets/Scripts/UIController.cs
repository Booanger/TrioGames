using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void LoadMenu(string sceneName)
    {

        StartCoroutine(LoadScreen(sceneName, 1));
    }

    private IEnumerator LoadScreen(string name, float time)
    {
        yield return new WaitForSeconds(time);
        //SceneManager.LoadScene(name);
        //SceneManager.UnloadScene(name);
        SceneManager.LoadScene(name);
    }
}