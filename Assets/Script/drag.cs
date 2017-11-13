using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour {

    private bool IsLocked = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsLocked)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objectPosition;
        }
        
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(gameObject.name))
        {
            transform.position = collision.gameObject.transform.position;
            IsLocked = true;
        }
    }
}
