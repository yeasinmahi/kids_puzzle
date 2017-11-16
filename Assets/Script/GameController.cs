using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    private RaycastHit2D hit;
    public bool IsLocked = false;
    void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        Drag();


    }
    public void Drag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        }

        if (Input.GetMouseButton(0))
        {
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
