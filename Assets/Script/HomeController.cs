using UnityEngine;
using UnityEngine.UI;
public class HomeController : MonoBehaviour {

    public bool isMoved = false;
    public static HomeController instance;
    public AudioClip backgroundSound,scrollingSound;
    public AudioSource AudioSource;
    public bool isMute;
    public Button MuteButton;
    public Sprite mike;
    public Sprite mike_disable;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            AudioSource = GetComponent<AudioSource>();
            PlayBackgroundSound();
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
        //DontDestroyOnLoad(gameObject);
    }

    public void PlayBackgroundSound()
    {
        AudioSource.clip = backgroundSound;
        AudioSource.loop = true;
        AudioSource.Play();
    }

    public void MuteAudio()
    {
        if (AudioListener.pause==true)
        {
            AudioListener.pause = false;
            MuteButton.image.sprite = mike;
            return;
        }
        AudioListener.pause = true;
        MuteButton.image.sprite = mike_disable;
    }

    // Update is called once per frame
    void Update () {
        if (isMoved == true && AudioListener.pause == false)
        {
            AudioSource.PlayOneShot(scrollingSound, 0.7F);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
}
