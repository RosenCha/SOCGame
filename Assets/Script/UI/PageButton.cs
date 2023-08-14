using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PageButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image BG;
    public Text text;
    public Color grey;
    public Color yellow;
    public UI ui;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        BG.color = Color.yellow;
        if (text != null)
        {
            text.color = Color.yellow;
        }

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        BG.color = Color.gray;
        if (text != null)
        {
            text.color = Color.gray;
        }

    }

    public void IniButton()
    {
        BG.color = Color.gray;
        if (text != null)
        {
            text.color = Color.gray;
        }

    }
}
