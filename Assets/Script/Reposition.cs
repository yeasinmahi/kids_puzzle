using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour {

    private Vector2 currentPosition;
    
	void Start () {
        currentPosition = transform.position;
	}

	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            if (!GameController.instance.IsLocked)
            {
                transform.position = currentPosition;
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameController.instance.IsLocked = true;
            transform.position = collision.transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameController.instance.IsLocked = true;
            transform.position = collider2D.transform.position;
        }
    }
}
