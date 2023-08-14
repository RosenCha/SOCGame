using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Image BG;
    public Text text;
    public Color grey;
    public Color yellow;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        BG.color = new Color(0.23f, 1, 0, 1);
        if (text != null)
        {
            text.color = new Color(0.23f, 1, 0, 1);
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
