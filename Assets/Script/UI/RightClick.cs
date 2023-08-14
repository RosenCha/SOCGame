using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightClick : MonoBehaviour,IPointerClickHandler
{

    public EquipMentBlank blank;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            blank.ClickBlank(false);
        }
        else
        {
            blank.ClickBlank(true);
        }
    }
}
