using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenContoller : MonoBehaviour {

    public static LoadingScreenContoller instance = null;
    public Slider progressBar;
    private float totalTime = 5f;
    private float duration;
    private float progressbarMaxValue;


    void Awake () {
        if (instance == null)
        {
            instance = this;
            duration = totalTime;
            progressbarMaxValue = progressBar.maxValue;
            //Texture2D graph = ImageManager.LoadTexureFromResource("0");
            //ImageManager.ConvertToGrayscale(graph);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	void Update () {
        if (duration <= 0)
        {
            SceneManager.LoadScene("Home");
        }
        else
        {
            duration -= Time.deltaTime;
            progressBar.value = (progressbarMaxValue / totalTime * duration);
        }
	}
}
