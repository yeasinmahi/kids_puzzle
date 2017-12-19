using System.Collections;
using System.Collections.Generic;
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
            progressBar.value = (1 / totalTime * duration);
        }
	}
}
