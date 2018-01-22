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
    private int achivedToy;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            AudioSource = GetComponent<AudioSource>();
            achivedToy = SqliteManager.GetTotalAchivedToy();
            PlayBackgroundSound();
            List<World> worlds = SqliteManager.GetWorlds();
            ListPositionCtrl.Instance.listBoxes = new ListBox[worlds.Count];
            CreateWorlds(worlds);
            SetContentId(worlds);
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

   
    public void CreateWorlds(List<World> worlds)
    {
        foreach(World world in worlds)
        {
            CreateWorld(world);
        }
        
        //image.sprite = ImageManager.LoadImageFromTexure2D(ImageManager.LoadImageFromSprite("1.jpg")).sprite;

    }
    public void SetContentId(List<World> worlds)
    {
        int cnt = 0;
        foreach(World world in worlds)
        {
            if (cnt <= 0)
            {
                ListPositionCtrl.Instance.listBoxes[cnt].nextListBox = ListPositionCtrl.Instance.listBoxes[cnt + 1];
                ListPositionCtrl.Instance.listBoxes[cnt].lastListBox = ListPositionCtrl.Instance.listBoxes[worlds.Count - 1];
            }
            else if (cnt >= worlds.Count - 1)
            {
                ListPositionCtrl.Instance.listBoxes[cnt].nextListBox = ListPositionCtrl.Instance.listBoxes[0];
                ListPositionCtrl.Instance.listBoxes[cnt].lastListBox = ListPositionCtrl.Instance.listBoxes[cnt - 1];
            }
            else
            {
                ListPositionCtrl.Instance.listBoxes[cnt].nextListBox = ListPositionCtrl.Instance.listBoxes[cnt + 1];
                ListPositionCtrl.Instance.listBoxes[cnt].lastListBox = ListPositionCtrl.Instance.listBoxes[cnt - 1];
            }
            cnt++;
        }
        
    }
    public void CreateWorld(World world)
    {
        Others.WriteLogInText("Create world start" + world.Sl);
        Vector3 pos = new Vector3(0, 0, 0);
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        Others.WriteLogInText("instantiate prefabs" + world.Sl);
        go.transform.SetParent(listBank.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponent<Image>().sprite = ImageManager.GetSprite(world.Image);
        go.GetComponent<Button>().onClick.AddListener(delegate { WorldOnClick(go); });
        go.GetComponent<ChangeItemImage>().sl = world.Sl;
        if (achivedToy >= world.TargetedToy)
        {
            go.GetComponent<ChangeItemImage>().isLock = false;
        }
        else
        {
            go.GetComponent<ChangeItemImage>().isLock = true;
        }
        ListBox listBox = go.GetComponent<ListBox>();
        listBox.listBoxID = counter;
        listBox.content.text = world.Name;

        AddWorld(listBox);
        Others.WriteLogInText("Complete World" + world.Sl);
    }

    private void WorldOnClick(GameObject worldGameObject)
    {
        if (!worldGameObject.GetComponent<ChangeItemImage>().isLock)
        {
            Others.worldId = worldGameObject.GetComponent<ChangeItemImage>().sl;
            MySceneManager.LoadInsideWorld();
        }
        
    }

    private int counter = 0;
    public void AddWorld(ListBox listBox)
    {
        ListPositionCtrl.Instance.listBoxes[counter] = listBox;
        counter++;
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
