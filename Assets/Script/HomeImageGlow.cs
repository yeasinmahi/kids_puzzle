using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HomeImageGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
    }
}

