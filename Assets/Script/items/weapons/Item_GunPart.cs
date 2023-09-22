using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_GunPart : item
{
    /// <summary>
    /// �Ƿ���ר�����
    /// </summary>
    public bool SpecificOrNot;
    /// <summary>
    /// �Ƿ����չ
    /// </summary>
    public bool Extendable;
    /// <summary>
    /// �ɰ�װ����б�(����)
    /// </summary>
    /// <returns></returns>
    public List<GameObject> ExtendableList_obj;
    /// <summary>
    /// �ɰ�װ����б�(ֵ)
    /// </summary>
    public List<Item_GunPart> ExtenableList_GunPart
    {
        get
        {
            if (ExtendableList_obj.Count <= 0)
            {
                return null;
            }
            else
            {
                List<Item_GunPart> l = new List<Item_GunPart>();
                foreach (var item in ExtendableList_obj)
                {
                    l.Add(item.GetComponent<Item_GunPart>());
                }
                return l;
            }
        }
    }

    /// <summary>
    /// �Ѿ���װ��Щ��չ���(����)
    /// </summary>
    public List<GameObject> ExtendList_Obj;
    /// <summary>
    /// ��չ��������б�(ֵ)
    /// </summary>
    public List<Item_GunPart> ExtendList_GunPart
    {
        get
        {
            if (ExtendList_Obj.Count <= 0)
            {
                return null;
            }
            else
            {
                List<Item_GunPart> l = new List<Item_GunPart>();
                foreach (var item in ExtendList_Obj)
                {
                    l.Add(item.GetComponent<Item_GunPart>());
                }
                return l;
            }
        }
    }

    /// <summary>
    /// ���԰�װ���
    /// </summary>
    /// <param name="gameObject"></param>
    public void TryInstallPart(GameObject gameObject)
    {
        //���жϸ�����Ƿ���԰�װ
        bool b = false;
        foreach (var item in ExtenableList_GunPart)
        {
            if (gameObject.GetComponent<Item_GunPart>().ID == item.ID)
            {
                b = true;
                break;
            }
        }
        if (b == true)
        {
            gameObject.transform.SetParent(this.transform);
            ExtendList_Obj.Add(gameObject);
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    public Sprite Picture;
    /// <summary>
    /// ��װ������
    /// </summary>
    public List<Transform> ExtraPosList;
}
