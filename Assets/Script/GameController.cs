using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool onDrag = false;
    public static GameController instance = null;
    public string dragObjectName = string.Empty;
    public AudioSource gameAudio;
    public AudioClip matchingSound, mismatchingSound, scrollingSound;
    public Canvas forgroundCanvas;
    public GameObject ColoredImages;
    public float maxDuration = 30f;
    public float duration;
    public Text timerDisplayText;
    public Slider progressBar;
    public float progressbarCurrentValue;
    public float progressbarMaxValue = 100f;
    public bool isPaused=true;
    public GameObject PauseCanvas;
    public GameObject InformationCanvas;
    public Image InformationImage;
    public Image pauseImage;
    public GameObject DelayDisplayCanvas;
    public Text DelayDisplayText;
    public GameObject PlayAgainCanvas;
    public GameObject BackButtonCanvas;
    public GameObject GameOverCanvas;
    public GameObject ErrorCanvas;
    public Image GameOverImage;
    public Button MuteButton;
    public Sprite mike;
    public Sprite mike_disable;

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

    public int countDownTime = 1; 

    public Transform currentParrent;

    public float threeStarTime;
    public float twoStarTime;
    public static int currentStar = 3;

    public GameObject toy1;
    public GameObject toy2;
    public GameObject toy3;

    public GameObject gameOverToy1;
    public GameObject gameOverToy2;
    public GameObject gameOverToy3;


    public string gameImageSourceColor = "5";
    public string gameImageSourceBlack = "5.b";
    public int matchCounter = 0;

    InsideWorld insideWorld = null;
    public bool isGameDataReady = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            Init();
            if (isGameDataReady)
            {
                ReadyGamePlay();
                gameAudio = GetComponent<AudioSource>();
                progressBar.maxValue = progressbarMaxValue;
                progressBar.value = progressbarMaxValue;
                duration = maxDuration;
                threeStarTime = progressbarMaxValue / 1.5f;
                twoStarTime = progressbarMaxValue / 2;
                StartCoroutine(Countdown(countDownTime));
            }
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (AudioListener.pause == true)
        {
            MuteButton.image.sprite = mike_disable;
        }
        else
        {
            MuteButton.image.sprite = mike;
        }
    }

    private void Init()
    {
        PauseCanvas.SetActive(false);
        InformationCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        DelayDisplayCanvas.SetActive(false);
        PlayAgainCanvas.SetActive(false);
        BackButtonCanvas.SetActive(false);
        if (Others.insideWorldId > 0)
        {
            insideWorld = SqliteManager.GetInsideWorld(Others.insideWorldId);
            if (insideWorld != null)
            {
                sourceColor = ImageManager.LoadTexureFromResource(insideWorld.ColorImage);
                sourceBlack = ImageManager.LoadTexureFromResource(insideWorld.BWImage);
                isGameDataReady = true;
                ErrorCanvas.SetActive(false);
            }
            else
            {
                isGameDataReady = false;
                ErrorCanvas.SetActive(true);
            }
        }
        else
        {
            isGameDataReady = false;
            ErrorCanvas.SetActive(true);
        }
        
        
    }

    void Update () {
        if (!isPaused)
        {
            if (isGameDataReady)
            {
                if (duration <= 0.0f)
                {
                    TimeEnd();
                }
                else
                {
                    duration -= Time.deltaTime;
                    progressbarCurrentValue = CalculateCurrentProgressValue();
                    currentStar = CalculateStar();
                    ManageToyStar(currentStar);
                    if (matchCounter >= 4)
                    {
                        GameOver();
                    }
                    progressBar.value = progressbarCurrentValue;
                }
                timerDisplayText.text = Math.Round(duration, 0).ToString();
            }
        }
        if (onDrag == true && AudioListener.pause == false)
        {
            gameAudio.PlayOneShot(scrollingSound, 0.7F);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void MuteAudio()
    {
        if (AudioListener.pause == true)
        {
            AudioListener.pause = false;
            MuteButton.image.sprite = mike;
            return;
        }
        AudioListener.pause = true;
        MuteButton.image.sprite = mike_disable;
    }
    public int CalculateStar()
    {
        if (progressbarCurrentValue > threeStarTime)
        {
            return 3;
        }
        if (progressbarCurrentValue > twoStarTime)
        {
            return 2;
        }
        return 1;
    }
    public void ManageToyStar(int currentStar)
    {
        if (currentStar == 2)
        {
            toy3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
        }else if (currentStar == 1)
        {
            toy2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
        }

    }
    public void ManageGameOverToyStar(int currentStar)
    {
        if (currentStar == 2)
        {
            gameOverToy3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
        }
        else if (currentStar == 1)
        {
            gameOverToy3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            gameOverToy2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
        }

    }
    IEnumerator Countdown(int count)
    {
        DelayDisplayCanvas.SetActive(true);
        DelayDisplayText.fontSize = 150;
        while (count >= 0)
        {
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
        Sprite[] colorSprites = Others.SliceTexureintoSprite(sourceColor, 2);
        colorSprites = Others.Reshuffle(colorSprites);
        colorImage0.GetComponent<Image>().sprite = colorSprites[0];
        colorImage1.GetComponent<Image>().sprite = colorSprites[1];
        colorImage2.GetComponent<Image>().sprite = colorSprites[2];
        colorImage3.GetComponent<Image>().sprite = colorSprites[3];

        Sprite[] blackSprites = Others.SliceTexureintoSprite(sourceBlack, 2);
        blackImage0.GetComponent<Image>().sprite = blackSprites[0];
        blackImage1.GetComponent<Image>().sprite = blackSprites[1];
        blackImage2.GetComponent<Image>().sprite = blackSprites[2];
        blackImage3.GetComponent<Image>().sprite = blackSprites[3];
        
    }
    private Texture2D MakeBlackAndWhite(Texture2D texture) 
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
        isPaused = true;
        GameOverImage.sprite = Others.CreateSpriteFromTexture(sourceColor);
        ManageGameOverToyStar(currentStar);
        int achievedToy = SqliteManager.GetAchievedToy(Others.insideWorldId);
        
        if ( achievedToy > 0 && currentStar > achievedToy)
        {
            SqliteManager.UpdateIsCompleteAndToyIntoInsideWorld(Others.insideWorldId, 1, currentStar);
        }
        GameOverCanvas.SetActive(true);
    }

    public IEnumerator OnPauseGame()
    {
        StartCoroutine(Screenshoot.takeScreenShot());
        yield return new WaitForEndOfFrame();
        Screenshoot.LoadImages(pauseImage);
        yield return new WaitForEndOfFrame();
        isPaused = true;
        PauseCanvas.SetActive(true);
    }

    public void OnResumeGame()
    {
        StartCoroutine(Countdown(countDownTime));
        PauseCanvas.SetActive(false);
    }
    public void OnRestartGame()
    {

    }
    public void OnActiveInformation()
    {

        isPaused = true;
        InformationImage.sprite = Others.CreateSpriteFromTexture(sourceColor);
        InformationCanvas.SetActive(true);
    }
    public void OnCloseInformation()
    {
        isPaused = false;
        InformationCanvas.SetActive(false);
    }
    public void OnActiveError()
    {
        isPaused = true;
        ErrorCanvas.SetActive(true);
    }
    public void OnCloseError()
    {
        isPaused = false;
        ErrorCanvas.SetActive(false);
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
        if (audioType.Equals(Others.MyAudioType.Matching) && AudioListener.pause == false)
        {
            gameAudio.PlayOneShot(matchingSound);
        }else if (audioType.Equals(Others.MyAudioType.Mismatching) && AudioListener.pause == false)
        {
            gameAudio.PlayOneShot(mismatchingSound);
        }
    }
    public void SaveImageToGallery()
    {
        SaveIamgeToDefaultPath();
        ImageManager.MoveAsset(ImageManager.GetImageSaveLocation(), ImageManager.externalFolderLocation, ImageManager.fileName,ImageManager.ActionType.Move);
    }
    public void SaveIamgeToDefaultPath()
    {
        ImageManager.SaveImage(sourceColor, ImageManager.GetImageSaveLocation(), ImageManager.fileName);
    }

    //public void OnDrag(PointerEventData EventData)
    //{
    //    if (!isPaused)
    //    {
    //        if (EventData.dragging)
    //        {

    //            transform.position = EventData.position;
    //            List<GameObject> gameObjects = EventData.hovered;
    //            foreach (GameObject gameObject in gameObjects)
    //            {
    //                if (gameObject.tag.Equals("drag"))
    //                {
    //                    GameController.instance.dragObjectName = gameObject.name;
    //                }
    //            }

    //        }
    //    }
    //}
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    List<GameObject> gameObjects = eventData.hovered;
    //    foreach (GameObject gameObject in gameObjects)
    //    {
    //        if (gameObject.tag.Equals("drag"))
    //        {
    //            gameObject.transform.parent = currentParrent;
    //        }
    //    }
    //}

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    transform.parent = forgroundCanvas.transform;
    //}
}
