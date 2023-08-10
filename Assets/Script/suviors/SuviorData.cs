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

    /// <summary>
    /// װ������
    /// </summary>
    /// <param name="str">����Ԥ�����·��</param>
    public void EquipBackPack(string str)
    {
        GameObject o = Instantiate(Resources.Load<GameObject>(str));
        item_Equipment_BackPack script = o.GetComponent<item_Equipment_BackPack>();
        EquipEquipment(script);
        animCtrl.bagSpriteRender.sprite = script.inGameSprite;
        backPack = script;
        Debug.Log("װ���˱���");
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
    /// ���Դ�����Ʒ
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public int TryStorgeItem(item item, int count)
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
        //�ȳ��Դ����·�
        if (clouth != null)
        {
            extraNum -= clouth.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }
        //Ȼ���Դ��뱳��
        if (backPack != null)
        {
            extraNum -= backPack.TryStorageItem(item, count);
            if (extraNum == 0)
            {
                return 0;
            }
        }
        //����Դ������
        if (outer != null)
        {
            extraNum -= outer.TryStorageItem(item, count);
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
