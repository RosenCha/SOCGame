using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_GunPart : item
{
    /// <summary>
    /// 是否是专属配件
    /// </summary>
    public bool SpecificOrNot;
    /// <summary>
    /// 是否可拓展
    /// </summary>
    public bool Extendable;
    /// <summary>
    /// 可安装组件列表(引用)
    /// </summary>
    /// <returns></returns>
    public List<GameObject> ExtendableList_obj;
    /// <summary>
    /// 可安装组件列表(值)
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
    /// 已经安装哪些拓展组件(引用)
    /// </summary>
    public List<GameObject> ExtendList_Obj;
    /// <summary>
    /// 拓展组件引用列表(值)
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
    /// 尝试安装组件
    /// </summary>
    /// <param name="gameObject"></param>
    public void TryInstallPart(GameObject gameObject)
    {
        //先判断该组件是否可以安装
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
    /// 立绘
    /// </summary>
    public Sprite Picture;
    /// <summary>
    /// 安装点坐标
    /// </summary>
    public List<Transform> ExtraPosList;
}
