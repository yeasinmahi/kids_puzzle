using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reposition : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    private Vector2 currentPosition;
    private Vector2 obJectPosition;
    public bool isLocked = false;
    public string imageName = string.Empty;
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
                        HomeController.instance.AudioSource.volume = 0.5f;
                        GameController.instance.PlaySound(Others.MyAudioType.Mismatching);
                    }
                    else
                    {
                        transform.position = obJectPosition;
                        HomeController.instance.AudioSource.volume = 0.5f;
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
                foreach (GameObject gameObject in gameObjects)
                {
                    if (gameObject.tag.Equals("drag"))
                    {
                        GameController.instance.dragObjectName = gameObject.name;
                    }
                }

            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(name))
        {
            isLocked = true;
            obJectPosition = collision.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(name))
        {
            isLocked = false;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        List<GameObject> gameObjects = eventData.hovered;
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.tag.Equals("drag"))
            {
                gameObject.transform.parent = currentParrent;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = GameController.instance.forgroundCanvas.transform;
    }
}
