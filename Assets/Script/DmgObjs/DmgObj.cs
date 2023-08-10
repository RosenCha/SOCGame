using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����˵����壬��������������п��ܵ��˺������嶼������ű�
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
    /// �ı��������ֵ
    /// </summary>
    /// <param name="count">������</param>
    public void ChangeMaxHp(int count)
    {
        if (maxHp + count > 0)
        {
            maxHp += count;
        }
    }
    /// <summary>
    /// �ı令��
    /// </summary>
    /// <param name="count">������</param>
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
