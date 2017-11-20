using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour{

    public static GameController instance = null;
    public string dragObjectName = string.Empty;
    AudioSource gameAudio;
    public AudioClip backgroundSound, matchingSound, mismatchingSound;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gameAudio = GetComponent<AudioSource>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	void Update () {

    }
    public enum MyAudioType{
        Matching,
        Mismatching,
        Background
    }
    public void PlaySound(MyAudioType audioType)
    {
        if (audioType.Equals(MyAudioType.Matching))
        {
            gameAudio.PlayOneShot(matchingSound);
        }else if (audioType.Equals(MyAudioType.Mismatching))
        {
            gameAudio.PlayOneShot(mismatchingSound);
        }else if (audioType.Equals(MyAudioType.Background))
        {
            gameAudio.PlayOneShot(backgroundSound);
        }
        
    }
}
