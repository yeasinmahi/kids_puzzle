using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class Disable : MonoBehaviour {
    public Button MuteButton;
    public Sprite mike;
    public Sprite mike_disable;
    private int counter = 0;
    // Use this for initialization
    void Start () {
        MuteButton = GetComponent<Button>();
    }
	public void ChangeButton ()
    {
        counter++;
        if (counter % 2 == 0)
        {
            MuteButton.image.overrideSprite = mike;
        }
        else
        {
            MuteButton.image.overrideSprite = mike_disable;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
