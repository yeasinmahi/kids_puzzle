using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsideWorldController : MonoBehaviour {

    public bool startDrag = false;
    public static InsideWorldController instance;
    public AudioClip backgroundSound, scrollingSound;
    public AudioSource AudioSource;
    public bool isMute;
    public Button MuteButton;
    public Sprite mike;
    public Sprite mike_disable;
    public bool isDraged = false;
    public GameObject image;
    public GameObject hoveredImage;
    public int worldId;
    public GameObject prefab;
    public GameObject itemParent;
    private bool isShowImage = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            worldId = Others.worldId;
            if (worldId>0)
            {
                LoadInsideWorld();
            }
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
    }

    private void LoadInsideWorld()
    {
        List<InsideWorld> insideWorlds = SqliteManager.GetInsideWorlds(worldId);
        foreach(InsideWorld insideWorld in insideWorlds)
        {
            CreateLavel(insideWorld);
        }
    }

    private void CreateLavel(InsideWorld insideWorld)
    {
        Vector3 pos = new Vector3(0, 0, 0);
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        go.transform.SetParent(itemParent.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
        ImageManager.LoadSpriteFromResource(insideWorld.ColorImage, go.GetComponent<Image>().sprite);
        go.GetComponent<ChangeItemImage>().sl = insideWorld.Sl;
        if (insideWorld.IsComplete.Equals(0))
        {
            if (!isShowImage)
            {
                image.GetComponent<Image>().sprite = go.GetComponent<Image>().sprite;
                Others.insideWorldId = insideWorld.Sl;
                //go.GetComponentInChildren<Button>().gameObject.SetActive(false);
                go.GetComponent<ChangeItemImage>().isLock = false;
                isShowImage = true;
            }
            else
            {
                go.GetComponent<ChangeItemImage>().isLock = true;
                //go.GetComponentInChildren<Button>().gameObject.SetActive(true);
            }
        }
        else if (insideWorld.IsComplete.Equals(1))
        {
            go.GetComponent<ChangeItemImage>().achivedToy = insideWorld.AchievedToy; 
            go.GetComponent<ChangeItemImage>().isLock = false;
            
            //go.GetComponentInChildren<Button>().gameObject.SetActive(false);
        }

    }

    public void PlayBackgroundSound()
    {
        AudioSource.clip = backgroundSound;
        AudioSource.loop = true;
        AudioSource.Play();
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

    // Update is called once per frame
    void Update () {

        if(startDrag == true && AudioListener.pause == false)
        {
            AudioSource.PlayOneShot(scrollingSound, 0.7F);
        }
     

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!isDraged)
            {
                if (hoveredImage != null)
                {
                    image.GetComponent<Image>().sprite = hoveredImage.GetComponent<Image>().sprite;
                    Others.insideWorldId = hoveredImage.GetComponent<ChangeItemImage>().sl;
                }
            }
            isDraged = false;
        }
    }
}
