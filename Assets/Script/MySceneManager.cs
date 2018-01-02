using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

    public static new GameObject gameObject;
	public static void LoadScene(string sceneName)
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
    public void BackButton()
    {
        gameObject.SendMessage("OnBackButton", SendMessageOptions.DontRequireReceiver);
    }
    public void BackYes()
    {
        LoadScene("InsideWorld");
    }
    public void BackNo()
    {
        gameObject.SendMessage("OnBackNo", SendMessageOptions.DontRequireReceiver);
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
        Screenshoot.takeScreenShot();

    }
    public void Share()
    {

    }
    public void Save()
    {
        GameController.instance.SaveImageToGallery();
    }
    public static void LoadInsideWorld()
    {
        if (!HomeController.instance.isMoved)
        {
            LoadScene("InsideWorld");
        }
    }

    public void Mute()
    {
        HomeController.instance.MuteAudio();
    }
}
