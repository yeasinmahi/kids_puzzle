using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour{

    public static GameController instance = null;
    public string dragObjectName = string.Empty;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	void Update () {

    }
}
