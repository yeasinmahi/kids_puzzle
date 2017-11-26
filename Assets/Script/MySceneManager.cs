using UnityEngine;
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
        //Object[] objects = FindObjectsOfType(typeof(GameObject));
        //foreach (GameObject go in objects)
        //{
        //    go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        //}
    }
    public void ResumeGame()
    {
        gameObject.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
    }
}
