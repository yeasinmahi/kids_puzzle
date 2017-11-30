using UnityEngine;

public class HomeController : MonoBehaviour {

    public bool isMoved = false;
    public static HomeController instance;
    public AudioClip backgroundSound;
    public AudioSource AudioSource;
    public bool isMute;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PlayBackgroundSound();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PlayBackgroundSound()
    {
        AudioSource.clip = backgroundSound;
        AudioSource.loop = true;
        AudioSource.Play();
    }

    public void MuteAudio()
    {
        isMute = !isMute;
        AudioListener.pause = isMute ? false : true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
