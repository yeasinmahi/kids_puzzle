using UnityEngine;


public class GameController : MonoBehaviour{

    public static GameController instance = null;
    public string dragObjectName = string.Empty;
    public AudioSource gameAudio;
    public AudioClip backgroundSound, matchingSound, mismatchingSound;
    public Canvas forgroundCanvas;
    public GameObject ColoredImages;
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
