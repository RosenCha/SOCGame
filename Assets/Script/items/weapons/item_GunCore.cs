using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 所有枪的父类
/// </summary>
public class item_GunCore : item
{
    public Sprite weaponImage;

    public bool MainOrNot;
    /// <summary>
    /// 美术类型，0步枪1轻型2重型
    /// </summary>
    public int ArtType;
    /// <summary>
    /// 射击模式，0半自动1全自动2爆发3全自动爆发
    /// </summary>
    public int ShotMod;
    public gunCal GunCal;
    public gunType GunType;
    public int MaxAmmo;
    /// <summary>
    /// 后座力，越大就越不精准
    /// </summary>
    public float recoil;
    public float recoilAdd;
    /// <summary>
    /// 后座力回复，越大就回复越快
    /// </summary>
    public float recoilReturn;
    public float CoolTime;
    /// <summary>
    /// 每次开火射击多少发子弹，默认1，如果大于1就是霰弹枪
    /// </summary>
    public int BulletCount;
    public float BurstCool;
    public int BurstNum;
    public float ReloadTime;
    public int Damage;
    public int PierceCount;
    public float BulletSpeed;
    public SpriteRenderer spriteRenderer;
    public Sprite none;
    public  bool ShowImage;
    public Transform ShotPos;
    public GameObject GunPrefab;
    public GameObject BulletPrefab;
    /// <summary>
    /// 子弹的物品ID
    /// </summary>
    private string bulletItemID;

    public string BulletItemID
    {
        get {
            switch (GunCal)
            {
                case gunCal._9mm:
                    return "03001";
                case gunCal.point22:
                    return "03002";
                case gunCal.point45:
                    return "03003";
                case gunCal._556:
                    return "03004";
                case gunCal._762:
                    return "03005";
                case gunCal._4mm:
                    return "03006";
                case gunCal._338:
                    return "03007";
                case gunCal._127:
                    return "03008";
                case gunCal.grenade:
                    return "03009";
                case gunCal._4mmSmart:
                    return "03010";
                case gunCal._8mmSmart:
                    return "03011";
                case gunCal.shotgunShell:
                    return "03012";
                case gunCal.arrow:
                    return "03013";
                default:
                    return null;
                    
            }
        }
    }

    //配件相关属性
    public bool prototypeOrNot;
    public bool FullArmedOrNot;
    public bool ReplaceableBarrel;
    public List<GameObject> BarrelList;
    public string BarrelId;
    public bool ReplaceableStock;
    public List<GameObject> StockList;
    public string stockId;
    public bool ReplaceableHandgurad;
    public List<GameObject> HandguardList;
    public string handguardId;
    public bool ReplaceableMag;
    public List<GameObject> MagList;
    public string MagId;
    public bool ReplaceableUprail;
    public List<GameObject> UpRailList;
    public bool ReplaceableSpecificPart;
    public string UprailId;
    public List<GameObject> SpecificPartList;
    public string SpecificPartId;



    public void ActiveWeaponSprite()
    {
        if (ShowImage==true)
        {
            spriteRenderer.sprite = none;
            ShowImage = false;
        }
        else
        {
            spriteRenderer.sprite = weaponImage;
            ShowImage = true;
            
        }
    }
    public enum gunCal
    {
        _9mm,
        point22,
        point45,
        _556,
        _762,
        _4mm,
        _338,
        _127,
        grenade,
        _4mmSmart,
        _8mmSmart,
        shotgunShell,
        arrow
    }


    public enum gunType
    {
        lightPistol,
        heavyPistol,
        PDW,
        shotGun,
        rifle,
        AR,
        DMR,
        Sniper,
        LMG,
        HMG,
        others
    }

   
}
