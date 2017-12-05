using System;
using System.Collections;
using System.Collections.Generic;
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

    public Texture2D sourceColor;
    public Texture2D sourceBlack;
    public int slice = 2;
    public GameObject colorImage0;
    public GameObject colorImage1;
    public GameObject colorImage2;
    public GameObject colorImage3;
    public GameObject blackImage0;
    public GameObject blackImage1;
    public GameObject blackImage2;
    public GameObject blackImage3;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ReadyGamePlay();
            gameAudio = GetComponent<AudioSource>();
            progressBar.maxValue = progressbarMaxValue;
            progressBar.value = progressbarMaxValue;
            duration = maxDuration;
            StartCoroutine(Countdown(3));
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

    public void ReadyGamePlay()
    {
        Sprite[] colorSprites = SliceController.GetSliceGameObjectTexture(sourceColor, 2);

        colorImage0.GetComponent<Image>().sprite = colorSprites[0];
        colorImage1.GetComponent<Image>().sprite = colorSprites[1];
        colorImage2.GetComponent<Image>().sprite = colorSprites[2];
        colorImage3.GetComponent<Image>().sprite = colorSprites[3];

        Sprite[] blackSprites = SliceController.GetSliceGameObjectTexture(sourceBlack, 2);

        blackImage0.GetComponent<Image>().sprite = blackSprites[0];
        blackImage1.GetComponent<Image>().sprite = blackSprites[1];
        blackImage2.GetComponent<Image>().sprite = blackSprites[2];
        blackImage3.GetComponent<Image>().sprite = blackSprites[3];


    }
    private Texture2D makeBlackAndWhite(Texture2D texture) 
    {
        texture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 0.5f));
        texture.SetPixel(1, 0, Color.clear);
        texture.SetPixel(0, 1, Color.white);
        texture.SetPixel(1, 1, Color.black);
        texture.Apply();
        return texture;
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
