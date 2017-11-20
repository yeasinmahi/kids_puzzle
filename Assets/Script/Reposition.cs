using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reposition : MonoBehaviour, IDragHandler
{

    private Vector2 currentPosition;
    private Vector2 obJectPosition;
    public bool isLocked = false;
    public string imageName = string.Empty;

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (name.Equals(GameController.instance.dragObjectName))
            {
                if (!isLocked)
                {
                    transform.position = currentPosition;
                    GameController.instance.PlaySound(GameController.MyAudioType.Mismatching);
                }
                else
                {
                    transform.position = obJectPosition;
                    GameController.instance.PlaySound(GameController.MyAudioType.Matching);
                }
            }
            

        }
    }
    
    public void OnDrag(PointerEventData EventData)
    {
        
        if (EventData.dragging)
        {
            transform.position = EventData.position;
            List<GameObject> gameObjects = EventData.hovered;
            foreach(GameObject gameObject in gameObjects)
            {
                if (gameObject.tag.Equals("drag"))
                {
                    GameController.instance.dragObjectName = gameObject.name;
                }
            }
            
        };
        
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
}
