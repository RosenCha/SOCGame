using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 这是角色的背包脚本，这是一个逻辑意义上的背包，实际上存储着玩家数据
/// </summary>
public class SuviorData : MonoBehaviour
{
    //背包是哪个幸存者的
    [SerializeField]
    SuviorCtrl suviorCtrl;
    [SerializeField]
    DmgObj dmgObj;
    [SerializeField]
    SuviroAnimController animCtrl;
    private item_Gun mainWeapon;
    [SerializeField]
    //主武器
    public item_Gun MainWeapon
    {
        get { return mainWeapon; }
        set { mainWeapon = value; }
    }
    //副武器
    private item_Gun sideWeapon;
    public item_Gun SideWeapon
    {
        get { return sideWeapon; }
        set { sideWeapon = value; }
    }
    private item_Gun gunInUse;
    //正在使用的武器
    public item_Gun GunInUse
    {
        get { return gunInUse; }
        set { gunInUse = value; }
    }
    //近战武器
    private item_MeleeWeapon meleeWeapon;
    public item_MeleeWeapon MeleeWeaponEquiped
    {
        get { return meleeWeapon; }
        set { meleeWeapon = value; }
    }
    //背包
    private item_Equipment_BackPack backPack;

    public item_Equipment_BackPack BackPack
    {
        get { return backPack; }
    }
    //衣服
    private item_Equipment_Clouth clouth;

    public item_Equipment_Clouth Clouth
    {
        get { return clouth; }
    }
    //外挂
    private item_Equipment_Outer outer;

    public item_Equipment_Outer Outer
    {
        get { return outer; }
    }

    public int mainWeaponAmmo;
    public int sideWeaponAmmo;
    /// <summary>
    /// 额外物品堆叠数量，默认为0
    /// </summary>
    public int ExtraStack;





    /// <summary>
    /// 将武器从数据库装备到背包
    /// </summary>
    /// <param name="gun">枪械的预制体</param>
    public void EquipGun(GameObject gun)
    {
        GameObject o = Instantiate(gun);
        item_Gun gunScript = o.GetComponent<item_Gun>();
        Debug.Log("装备了枪支");
        if (gunScript.MainOrNot == true)
        {
            MainWeapon = gunScript;
            Debug.Log("装备了到主武器");
        }
        else
        {
            SideWeapon = gunScript;
            Debug.Log("装备到副武器");
        }
        if (gunScript.ShowImage == true)
        {
            gunScript.ActiveWeaponSprite();
        }
        suviorCtrl.ChangeWeapon(gunScript.MainOrNot);
    }

    public item_Equipment LoadEquipmentForResource(string str)
    {
        GameObject o = Instantiate(Resources.Load<GameObject>(str), this.transform);
        item_Equipment script = o.GetComponent<item_Equipment>();
        return script;
    }

    /// <summary>
    /// 装备"装备"
    /// </summary>

    public void EquipE(item_Equipment script)
    {

        switch (script.itemType)
        {
            case item.ItemType.bag4:
                item_Equipment_BackPack script1 = script.gameObject.GetComponent<item_Equipment_BackPack>();
                animCtrl.bagSpriteRender.sprite = script1.inGameSprite;
                backPack = script1;
                break;
            case item.ItemType.clothing5:
                item_Equipment_Clouth script2 = script.gameObject.GetComponent<item_Equipment_Clouth>();
                animCtrl.leftHandS = script2.leftHandS;
                animCtrl.rightHandS = script2.rightHandS;
                animCtrl.bodyS = script2.BodyS;
                animCtrl.leftArmS = script2.leftArmS;
                animCtrl.rightArmS = script2.rightArms;
                clouth = script2;
                break;
            case item.ItemType.coat6:
                item_Equipment_Outer script3 = script.gameObject.GetComponent<item_Equipment_Outer>();
                animCtrl.outerSpriterRender.sprite = script3.inGameSprite;
                outer = script3;
                break;
            default:
                break;
        }
        EquipEquipment(script);
    }
    public bool TryUnEquipE(item item)
    {
        int cellInNeed = 0;
        if(item.itemType== item.ItemType.bag4|| item.itemType == item.ItemType.clothing5|| item.itemType == item.ItemType.coat6)
        {
            cellInNeed = item.gameObject.GetComponent<item_Equipment>().SpaceCount;
            foreach (var item1 in item.gameObject.GetComponent<item_Equipment>().StorageSpace)
            {
                if (item1.Count == 0)
                {
                    cellInNeed--;
                }
            }
        }
        switch (item.itemType)
        {
            //case item.ItemType.gun2:
            //    int a = TryStorgeItem(item, 1, true, true, true);
            //    if (a == 0)
            //    {
            //        UnEquipE(item);
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
                //}
            case item.ItemType.bag4:
                if (CalcIfCouldStorge(cellInNeed, false, true, true) == true)
                {
                    UnEquipE(item);
                    return true;
                }
                else
                {
                    return false;
                }

            case item.ItemType.clothing5:
                if (CalcIfCouldStorge(cellInNeed, true, false, true) == true)
                {
                    UnEquipE(item);
                    return true;
                }
                else
                {
                    return false;
                }
            case item.ItemType.coat6:
                if (CalcIfCouldStorge(cellInNeed, true, true, false) == true)
                {
                    UnEquipE(item);
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }




    }
    public void UnEquipE(item item)
    {
        UnEquipEquipMent(item.gameObject.GetComponent<item_Equipment>());
        switch (item.itemType)
        {
            case item.ItemType.bag4:
                animCtrl.bagSpriteRender.sprite = animCtrl.bag_Basic;
                foreach (var item1 in item.gameObject.GetComponent<item_Equipment>().StorageSpace)
                {
                    if (item1.Count > 0)
                    {
                        TryStorgeItem(item1.ItemInCell, item1.Count, false, true, true);
                        item1.CleanCell();
                    }
                }
                TryStorgeItem(item, 1, false, true, true);
                backPack = null;
                break;
            case item.ItemType.clothing5:
                animCtrl.leftHandS = animCtrl.leftHandS_Basic;
                animCtrl.rightHandS = animCtrl.rightHandS_Basic;
                animCtrl.bodyS = animCtrl.bodyS_Basic;
                animCtrl.leftArmS = animCtrl.leftArmS_Basic;
                animCtrl.rightArmS = animCtrl.rightArmS_Basic;
                foreach (var item2 in item.gameObject.GetComponent<item_Equipment>().StorageSpace)
                {
                    if (item2.Count > 0)
                    {
                        TryStorgeItem(item2.ItemInCell, item2.Count, true, false, true);
                        item2.CleanCell();
                    }
                }
                TryStorgeItem(item, 1, true, false, true);
                clouth = null;
                break;
            case item.ItemType.coat6:
                animCtrl.outerSpriterRender.sprite = animCtrl.outer_Basic;
                foreach (var item3 in item.gameObject.GetComponent<item_Equipment>().StorageSpace)
                {
                    if (item3.Count > 0)
                    {
                        TryStorgeItem(item3.ItemInCell, item3.Count, true, true, false);
                        item3.CleanCell();
                    }
                }
                TryStorgeItem(item, 1, true,true, false);
                outer = null;
                break;
            default:
                break;
        }

    }

    private void EquipEquipment(item_Equipment itemScript)
    {
        dmgObj.ChangeMaxHp(itemScript.HpAdd);
        dmgObj.ChangeArmor(itemScript.DefenseAdd);
        itemScript.IniEquipMent();
    }

    private void UnEquipEquipMent(item_Equipment itemScript)
    {
        dmgObj.ChangeMaxHp(-itemScript.HpAdd);
        dmgObj.ChangeArmor(-itemScript.DefenseAdd);
    }
    /// <summary>
    /// 计算能否将一个容器内的物品全部放入其他容器内
    /// </summary>
    /// <param name="cellCount"></param>
    /// <returns></returns>
    public bool CalcIfCouldStorge(int cellCount,bool useBag,bool useClouth,bool useCoat)
    {
        int extarCell = 0;
        if(useBag==true&& backPack != null)
        {
            foreach (var item in backPack.StorageSpace)
            {
                if (item.Count == 0)
                {
                    extarCell++;
                }
            }
        }
        if (useClouth == true && clouth != null)
        {
            foreach (var item in clouth.StorageSpace)
            {
                if (item.Count == 0)
                {
                    extarCell++;
                }
            }
        }
        if (useCoat == true && outer != null)
        {
            foreach (var item in outer.StorageSpace)
            {
                if (item.Count == 0)
                {
                    extarCell++;
                }
            }
        }
        if (extarCell > cellCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 尝试存入物品
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public int TryStorgeItem(item item, int count,bool useBag,bool useClouth,bool useCoat)
    {
        //为了防止错误标注了不可堆叠物品的数量,这里如果物品不可堆叠就强制数量为1
        if (item.stackAble == false)
        {
            count = 1;
        }
        else if (count == 0)
        {
            Debug.Log("不能存入0个物体");
            return 0;
        }
        //extarNum表示往一件装备中存入东西后剩余的数量
        int extraNum = count;
        
        if (backPack != null && useBag==true)
        {
            extraNum -= extraNum- backPack.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }
        //先尝试存入衣服
        if (clouth != null&&useClouth==true)
        {
            extraNum -= extraNum- clouth.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }

        //最后尝试存入外挂
        if (outer != null&&useCoat==true)
        {
            extraNum -= extraNum- outer.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }
        //如果程序执行到这里就说明背包空间不够了,返回存不下的数量
        return extraNum;
    }

    /// <summary>
    /// 尝试从背包中取出一定数量的物品
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    /// <returns>返回的数量是成功取出的数量,如果为0表示该物品在背包里不存在</returns>
    public int TryGetItemThroughID(string id, int count)
    {
        int returnNum = 0;
        if (count == 0)
        {
            Debug.Log("不能取出0个物体");
            return 0;
        }
        if (clouth != null)
        {
            returnNum += clouth.TryGetItemThroughID(id, count - returnNum);
            if (returnNum == count)
            {
                return returnNum;
            }
        }
        if (backPack != null)
        {
            returnNum += backPack.TryGetItemThroughID(id, count - returnNum);
            if (returnNum == count)
            {
                return returnNum;
            }
        }
        if (outer != null)
        {
            returnNum += outer.TryGetItemThroughID(id, count - returnNum);
            if (returnNum == count)
            {
                return returnNum;
            }
        }
        return returnNum;
    }
}
