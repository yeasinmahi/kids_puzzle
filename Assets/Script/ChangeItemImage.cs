using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeItemImage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler {
    public GameObject image;
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
    }
}
