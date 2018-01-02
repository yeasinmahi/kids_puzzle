using System;
using System.Collections.Generic;
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
    public GameObject listBank;
    public GameObject prefab;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            AudioSource = GetComponent<AudioSource>();
            PlayBackgroundSound();
            List<World> worlds = SqliteManager.GetWorld();
            ListPositionCtrl.Instance.listBoxes = new ListBox[worlds.Count];
            CreateWorlds(worlds);
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
    public void CreateWorlds(List<World> worlds)
    {
        foreach(World world in worlds)
        {
            CreateWorld(world);
        }
        
        //image.sprite = ImageManager.LoadImageFromTexure2D(ImageManager.LoadImageFromSprite("1.jpg")).sprite;

    }
    
    public void CreateWorld(World world)
    {
        Vector3 pos = new Vector3(0, 0, 0);
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        go.transform.SetParent(listBank.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponent<Image>().sprite = ImageManager.LoadSpriteFromResource(world.WorldImage);
        go.GetComponent<Button>().onClick.AddListener(delegate { WorldOnClick(world.Sl); });
        ListBox listBox = go.GetComponent<ListBox>();
        listBox.listBoxID = counter;
        listBox.content.text = world.WorldName;

        AddWorld(listBox);
    }

    private void WorldOnClick(int worldId)
    {
        Others.worldId = worldId;
        MySceneManager.LoadInsideWorld();
    }

    private int counter = 0;
    public void AddWorld(ListBox listBox)
    {
        ListPositionCtrl.Instance.listBoxes[counter] = listBox;
        counter++;
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
