using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackPackPanel : MonoBehaviour
{
    public void IniBackPackPanel()
    {
        EquipMentBlank[] a = GetComponentsInChildren<EquipMentBlank>();
        foreach (var item in a)
        {
            item.IniEquipMentBlank();
        }
    }
    public void ReChose()
    {
        EquipMentBlank[] a = GetComponentsInChildren<EquipMentBlank>();
        foreach (var item in a)
        {
            item.ChoseBlank(false);
        }
    }

}
