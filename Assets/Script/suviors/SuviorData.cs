using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ǽ�ɫ�ı����ű�������һ���߼������ϵı�����ʵ���ϴ洢���������
/// </summary>
public class SuviorData : MonoBehaviour
{
    //�������ĸ��Ҵ��ߵ�
    [SerializeField]
    SuviorCtrl suviorCtrl;
    [SerializeField]
    DmgObj dmgObj;
    [SerializeField]
    SuviroAnimController animCtrl;
    private item_Gun mainWeapon;
    [SerializeField]
    //������
    public item_Gun MainWeapon
    {
        get { return mainWeapon; }
        set { mainWeapon = value; }
    }
    //������
    private item_Gun sideWeapon;
    public item_Gun SideWeapon
    {
        get { return sideWeapon; }
        set { sideWeapon = value; }
    }
    private item_Gun gunInUse;
    //����ʹ�õ�����
    public item_Gun GunInUse
    {
        get { return gunInUse; }
        set { gunInUse = value; }
    }
    //��ս����
    private item_MeleeWeapon meleeWeapon;
    public item_MeleeWeapon MeleeWeaponEquiped
    {
        get { return meleeWeapon; }
        set { meleeWeapon = value; }
    }
    //����
    private item_Equipment_BackPack backPack;

    public item_Equipment_BackPack BackPack
    {
        get { return backPack; }
    }
    //�·�
    private item_Equipment_Clouth clouth;

    public item_Equipment_Clouth Clouth
    {
        get { return clouth; }
    }
    //���
    private item_Equipment_Outer outer;

    public item_Equipment_Outer Outer
    {
        get { return outer; }
    }

    public int mainWeaponAmmo;
    public int sideWeaponAmmo;
    /// <summary>
    /// ������Ʒ�ѵ�������Ĭ��Ϊ0
    /// </summary>
    public int ExtraStack;





    /// <summary>
    /// �����������ݿ�װ��������
    /// </summary>
    /// <param name="gun">ǹе��Ԥ����</param>
    public void EquipGun(GameObject gun)
    {
        GameObject o = Instantiate(gun);
        item_Gun gunScript = o.GetComponent<item_Gun>();
        Debug.Log("װ����ǹ֧");
        if (gunScript.MainOrNot == true)
        {
            MainWeapon = gunScript;
            Debug.Log("װ���˵�������");
        }
        else
        {
            SideWeapon = gunScript;
            Debug.Log("װ����������");
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
    /// װ��"װ��"
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
    /// �����ܷ�һ�������ڵ���Ʒȫ����������������
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
    /// ���Դ�����Ʒ
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public int TryStorgeItem(item item, int count,bool useBag,bool useClouth,bool useCoat)
    {
        //Ϊ�˷�ֹ�����ע�˲��ɶѵ���Ʒ������,���������Ʒ���ɶѵ���ǿ������Ϊ1
        if (item.stackAble == false)
        {
            count = 1;
        }
        else if (count == 0)
        {
            Debug.Log("���ܴ���0������");
            return 0;
        }
        //extarNum��ʾ��һ��װ���д��붫����ʣ�������
        int extraNum = count;
        
        if (backPack != null && useBag==true)
        {
            extraNum -= extraNum- backPack.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }
        //�ȳ��Դ����·�
        if (clouth != null&&useClouth==true)
        {
            extraNum -= extraNum- clouth.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }

        //����Դ������
        if (outer != null&&useCoat==true)
        {
            extraNum -= extraNum- outer.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }
        //�������ִ�е������˵�������ռ䲻����,���ش治�µ�����
        return extraNum;
    }

    /// <summary>
    /// ���Դӱ�����ȡ��һ����������Ʒ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    /// <returns>���ص������ǳɹ�ȡ��������,���Ϊ0��ʾ����Ʒ�ڱ����ﲻ����</returns>
    public int TryGetItemThroughID(string id, int count)
    {
        int returnNum = 0;
        if (count == 0)
        {
            Debug.Log("����ȡ��0������");
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
