using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public string ID;
    public ItemType itemType;
    public Sprite ImageInPack;
    public string Name;
    public string Descirbe;
    public float Weight;
    public bool stackAble;
    public int stackCount;
    public enum ItemType
    {
        emptyObject0,
        meleeWeapon1,
        gun2,
        ammo3,
        bag4,
        clothing5,
        coat6,
        food7,
        med8,
        tool9
    }
}
