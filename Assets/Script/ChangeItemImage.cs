using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeItemImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int sl = 0;
    public bool isLock = true;
    public int achivedToy = 0;
    public void Start()
    {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.tag.Equals("upperImage"))
            {
                button.gameObject.SetActive(isLock);
                if (achivedToy > 0)
                {
                    button.gameObject.SetActive(true);
                    switch (achivedToy)
                    {
                        case 1:
                            button.gameObject.GetComponent<Image>().sprite = ImageManager.LoadSpriteFromResource("toy1");
                            break;
                        case 2:
                            button.gameObject.GetComponent<Image>().sprite = ImageManager.LoadSpriteFromResource("toy2");
                            break;
                        case 3:
                            button.gameObject.GetComponent<Image>().sprite = ImageManager.LoadSpriteFromResource("toy3");
                            break;
                        default:
                            button.gameObject.SetActive(false);
                            break;
                    }
                    
                }
            }
        }
        if (isLock)
        {
            gameObject.GetComponent<Image>().color = new Color32(50, 50, 50, 255);
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isLock)
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if (InsideWorldController.instance != null)
            {
                InsideWorldController.instance.hoveredImage = gameObject;
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isLock)
        {
            gameObject.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
            if (InsideWorldController.instance != null)
            {
                InsideWorldController.instance.hoveredImage = null;
            }
        }
        
    }
}
