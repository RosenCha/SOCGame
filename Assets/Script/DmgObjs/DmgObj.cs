using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可受伤的物体，包括玩家在内所有可受到伤害的物体都有这个脚本
/// </summary>
public class DmgObj : MonoBehaviour
{
    [SerializeField]
    private int hp;

    public int Hp
    {
        get { return hp; }
    }
    [SerializeField]
    private int maxHp;

    public int MaxHp
    {
        get { return maxHp; }
    }
    [SerializeField]
    private int armor;

    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    public void DamageHp(int count)
    {
        if (hp - count <= maxHp)
        {
            hp -= count;
        }
        else
        {
            hp = maxHp;
        }

    }

    /// <summary>
    /// 改变最大生命值
    /// </summary>
    /// <param name="count">增加量</param>
    public void ChangeMaxHp(int count)
    {
        if (maxHp + count > 0)
        {
            maxHp += count;
        }
    }
    /// <summary>
    /// 改变护甲
    /// </summary>
    /// <param name="count">增加量</param>
    public void ChangeArmor(int count)
    {
        if (armor + count >= 0)
        {
            armor += count;
        }
        else
        {
            armor = 0;
        }
    }
}
