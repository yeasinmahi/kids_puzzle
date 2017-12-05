using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]

public class InsideWorldController : MonoBehaviour {

    public bool isMoved = false;
    public static InsideWorldController bb;
    public AudioClip backgroundSound, scrollingSound;
    public AudioSource AudioSource;
    public bool isMute;
    public Button MuteButton;
    public Sprite mike;
    public Sprite mike_disable;
    
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

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
