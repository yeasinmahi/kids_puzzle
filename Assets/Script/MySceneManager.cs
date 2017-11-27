﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

    public GameObject gameObject;
	public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void PauseGame()
    {
        gameObject.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
    }
    public void ResumeGame()
    {
        gameObject.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
    }
    public void RestartGame()
    {
        LoadScene("Main");
        //gameObject.SendMessage("OnRestartGame", SendMessageOptions.DontRequireReceiver);
    }
    public void Home()
    {
        LoadScene("Home");
        //gameObject.SendMessage("OnRestartGame", SendMessageOptions.DontRequireReceiver);
    }
    public void ActiveInformationModal()
    {
        gameObject.SendMessage("OnActiveInformation", SendMessageOptions.DontRequireReceiver);
    }
    public void CloseInformationModal()
    {
        gameObject.SendMessage("OnCloseInformation", SendMessageOptions.DontRequireReceiver);
    }
    public void ScreenShootAndLoad()
    {

    }
    public void Share()
    {

    }
    public void Save()
    {

    }
    public void LoadInsideWorld()
    {
        if (!HomeController.instance.isMoved)
        {
            LoadScene("InsideWorld");
        }
    }
}