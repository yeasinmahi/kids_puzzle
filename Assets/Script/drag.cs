using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour {

    private bool IsLocked = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                Vector2 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = objectPosition;
            }
        }
        //if (Input.GetMouseButton(0))
        //{

        //    Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            

        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        //    if (hit.collider != null)
        //    {
        //        Vector2 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //        transform.position = objectPosition;
        //    }
        //}
        
	}

    public void GetRay()
    {
        
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
