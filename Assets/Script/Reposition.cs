using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Reposition : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    private Vector2 currentPosition;
    private Vector2 obJectPosition;
    private GameObject currentObject;
    public bool isMatched = false;
    public bool isLocked = false;
    public Transform currentParrent;


    void Start()
    {
        currentPosition = transform.position;
        currentParrent = transform.parent;

    }

    void Update()
    {
        if (!GameController.instance.isPaused)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (name.Equals(GameController.instance.dragObjectName))
                {

                    if (!isMatched)
                    {
                        transform.position = currentPosition;
                        if (HomeController.instance != null)
                        {
                            HomeController.instance.AudioSource.volume = 0.5f;
                        }
                        GameController.instance.PlaySound(Others.MyAudioType.Mismatching);
                    }
                    else
                    {
                        if (currentObject != null)
                        {
                            if (currentObject.activeSelf)
                            {
                                transform.position = obJectPosition;
                                if (HomeController.instance != null)
                                {
                                    HomeController.instance.AudioSource.volume = 0.5f;
                                }
                                isLocked = true;
                                GameController.instance.PlaySound(Others.MyAudioType.Matching);
                                GameController.instance.matchCounter++;
                            }
                            currentObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
    public void OnDrag(PointerEventData EventData)
    {
        if (!GameController.instance.isPaused)
        {
            if (!isLocked)
            {
                if (EventData.dragging)
                {
                    transform.position = EventData.position;
                    List<GameObject> gameObjects = EventData.hovered;
                    foreach (GameObject go in gameObjects)
                    {
                        if (go.tag.Equals("drag"))
                        {
                            string s = go.GetComponent<Image>().sprite.name;
                            GameController.instance.dragObjectName = go.name;
                            //Others.WriteDebugLog("debug:", go.name);
                            break;
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string s = collision.gameObject.GetComponent<Image>().sprite.name;
        if (collision.gameObject.GetComponent<Image>().sprite.name.Equals(GetComponent<Image>().sprite.name))
        {
            isMatched = true;
            obJectPosition = collision.transform.position;
            currentObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Image>().sprite.name.Equals(GetComponent<Image>().sprite.name))
        {
            if (!isLocked)
            {
                isMatched = false;
                currentObject = null;
            }
            
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            List<GameObject> gameObjects = eventData.hovered;
            foreach (GameObject go in gameObjects)
            {
                if (go.tag.Equals("drag"))
                {
                    go.transform.parent = currentParrent;
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isLocked)
        {
            transform.parent = GameController.instance.forgroundCanvas.transform;
        }
    }
}
