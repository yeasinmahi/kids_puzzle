using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour{

    public static GameController instance = null;
    public string dragObjectName = string.Empty;
    public AudioSource gameAudio;
    public AudioClip matchingSound, mismatchingSound;
    public Canvas forgroundCanvas;
    public GameObject ColoredImages;
    public float maxDuration = 10f;
    public float duration;
    public Text timerDisplayText;
    public Slider progressBar;
    public float progressbarCurrentValue;
    public float progressbarMaxValue = 100f;
    public bool isPaused=true;
    public GameObject PauseCanvas;
    public GameObject InformationCanvas;
    public Image pauseImage;
    public GameObject DelayDisplayCanvas;
    public Text DelayDisplayText;
    public GameObject PlayAgainCanvas;
    public GameObject BackButtonCanvas;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameAudio = GetComponent<AudioSource>();
            progressBar.maxValue = progressbarMaxValue;
            progressBar.value = progressbarMaxValue;
            duration = maxDuration;
            //StartCoroutine(Countdown(3));
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
                TimeEnd();
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
    IEnumerator Countdown(int count)
    {
        DelayDisplayCanvas.SetActive(true);
        DelayDisplayText.fontSize = 150;
        while (count >= 0)
        {

            // display something...
            if (count == 0)
            {
                DelayDisplayText.fontSize = 100;
                DelayDisplayText.text = "GO";
                
            }
            else
            {
                DelayDisplayText.text = count.ToString();
            }
            
            yield return new WaitForSeconds(1);
            count--;
        }

        // count down is finished...
        StartGame();
    }

    private void StartGame()
    {
        isPaused = false;
        DelayDisplayCanvas.SetActive(false);

    }

    private void TimeEnd()
    {
        isPaused = true;
        PlayAgainCanvas.SetActive(true);

    }
    private void GameOver()
    {
        progressbarCurrentValue = 0;
    }

    public IEnumerator OnPauseGame()
    {
        //TestCanvas.SetActive(true);
        //testText.text = Screenshoot.GetScreenshootSaveLocation();

        StartCoroutine(Screenshoot.takeScreenShot());
        yield return new WaitForEndOfFrame();
        Screenshoot.LoadImages(pauseImage);
        yield return new WaitForEndOfFrame();
        isPaused = true;
        PauseCanvas.SetActive(true);
    }

    public void OnResumeGame()
    {
        StartCoroutine(Countdown(3));
        PauseCanvas.SetActive(false);
    }
    public void OnRestartGame()
    {

    }
    public void OnActiveInformation()
    {
        isPaused = true;
        InformationCanvas.SetActive(true);
    }
    public void OnCloseInformation()
    {
        isPaused = false;
        InformationCanvas.SetActive(false);
    }
    float CalculateCurrentProgressValue()
    {
        return progressbarMaxValue / maxDuration * duration;
    }
    public void OnBackButton()
    {
        isPaused = true;
        BackButtonCanvas.SetActive(true);
    }
    public void OnBackNo()
    {
        isPaused = false;
        BackButtonCanvas.SetActive(false);
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
        }
    }
}
