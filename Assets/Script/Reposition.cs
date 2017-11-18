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
            if (!isLocked)
            {
                transform.position = currentPosition;
            }
            else
            {
                transform.position = obJectPosition;
            }

        }
    }
    public void OnDrag(PointerEventData EventData)
    {

        if (EventData.dragging)
        {
            transform.position = EventData.position;
        };
        //EventData.currentSelectedGameObject.transform.position = Input.mousePosition;
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
    void Get()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        }
    }
}
