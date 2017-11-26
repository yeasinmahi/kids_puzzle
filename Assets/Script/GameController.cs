using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour{

    public static GameController instance = null;
    public string dragObjectName = string.Empty;
    public AudioSource gameAudio;
    public AudioClip backgroundSound, matchingSound, mismatchingSound;
    public Canvas forgroundCanvas;
    public GameObject ColoredImages;
    public float maxDuration = 10f;
    public float duration;
    public Text timerDisplayText;
    public Slider progressBar;
    public float progressbarCurrentValue;
    public float progressbarMaxValue = 100f;
    public bool isPaused;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameAudio = GetComponent<AudioSource>();
            progressBar.maxValue = progressbarMaxValue;
            progressBar.value = progressbarMaxValue;
            duration = maxDuration;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Update () {
        if (!isPaused)
        {
            if (duration <= 0.0f)
            {
                GameOver();
            }
            else
            {
                duration -= Time.deltaTime;
                progressbarCurrentValue = CalculateCurrentProgressValue();
                progressBar.value = progressbarCurrentValue;
            }
            timerDisplayText.text = Math.Round(duration, 0).ToString();
        }
    }

    private void GameOver()
    {
        progressbarCurrentValue = 0;
    }
    

    public void OnPauseGame()
    {
        isPaused = true;
    }

    public void OnResumeGame()
    {
        isPaused = false;
    }
    
    float CalculateCurrentProgressValue()
    {
        return progressbarMaxValue / maxDuration * duration;
    }
    public Canvas GetForgroundCanvas()
    {
        Canvas[] canvases = transform.root.GetComponentsInChildren<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            if (canvas.name.Equals("ForgroundCanvas"))
            {
                return canvas;
            }
            
        }
        return null;
        //GameObject gameObject = ColoredImages.GetComponent<GameObject>();
    }
    public void PlaySound(Others.MyAudioType audioType)
    {
        if (audioType.Equals(Others.MyAudioType.Matching))
        {
            gameAudio.PlayOneShot(matchingSound);
        }else if (audioType.Equals(Others.MyAudioType.Mismatching))
        {
            gameAudio.PlayOneShot(mismatchingSound);
        }else if (audioType.Equals(Others.MyAudioType.Background))
        {
            gameAudio.PlayOneShot(backgroundSound);
        }
        
    }
}
