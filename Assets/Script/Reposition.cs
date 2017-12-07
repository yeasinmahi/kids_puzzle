using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Reposition : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    private Vector2 currentPosition;
    private Vector2 obJectPosition;
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
                    if (!isLocked)
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
                        transform.position = obJectPosition;
                        if (HomeController.instance != null)
                        {
                            HomeController.instance.AudioSource.volume = 0.5f;
                        }
                        GameController.instance.PlaySound(Others.MyAudioType.Matching);
                    }
                }
            }
        }
    }

    public void OnDrag(PointerEventData EventData)
    {
        if (!GameController.instance.isPaused)
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
                        Others.WriteDebugLog("debug:", go.name);
                        break;
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
            isLocked = true;
            obJectPosition = collision.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Image>().sprite.name.Equals(GetComponent<Image>().sprite.name))
        {
            isLocked = false;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = GameController.instance.forgroundCanvas.transform;
    }
}
