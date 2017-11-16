using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Drag();


    }
    public void Drag()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag.Equals("drag"))
                {
                    Vector2 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    hit.collider.gameObject.transform.position = objectPosition;
                }
            }
        }
    }
}
