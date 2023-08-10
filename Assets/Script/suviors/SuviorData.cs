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

    /// <summary>
    /// 装备背包
    /// </summary>
    /// <param name="str">背包预制体的路径</param>
    public void EquipBackPack(string str)
    {
        GameObject o = Instantiate(Resources.Load<GameObject>(str));
        item_Equipment_BackPack script = o.GetComponent<item_Equipment_BackPack>();
        EquipEquipment(script);
        animCtrl.bagSpriteRender.sprite = script.inGameSprite;
        backPack = script;
        Debug.Log("装备了背包");
    }

    public void UnEquipBackPack()
    {
        UnEquipEquipMent(backPack);
        animCtrl.bagSpriteRender.sprite = animCtrl.bag_Basic;
        Destroy(backPack.gameObject);
        backPack = null;
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
    /// 尝试存入物品
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public int TryStorgeItem(item item, int count)
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
        //先尝试存入衣服
        if (clouth != null)
        {
            extraNum -= clouth.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }
        //然后尝试存入背包
        if (backPack != null)
        {
            extraNum -= backPack.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }
        //最后尝试存入外挂
        if (outer != null)
        {
            extraNum -= outer.TryStorageItem(item, count);
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
