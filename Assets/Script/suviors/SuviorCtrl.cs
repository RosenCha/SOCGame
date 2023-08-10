using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player controller,which manage  the charactor logic 
public class SuviorCtrl : MonoBehaviour
{
    [SerializeField]
    //角色坐标
    Transform PlayerTrans;
    [SerializeField]
    //角色动画层
    Transform SuviorSpriteObj;
    [SerializeField]
    Transform rifleTrans;
    [SerializeField]
    Transform lightPistolTrans;
    [SerializeField]
    Transform heavyPistolTrans;
    Transform weaponFollowTrans;
    Transform currentShotPoint;
    [SerializeField]
    Rigidbody2D rigidbody2D;

    [SerializeField]
    SuviroAnimController suviroAnimCtrl;

    [SerializeField]
    Transform Foot;
    [SerializeField]
    Transform Body;
    bool isAiming;
    private int gunType;

    //玩家使用的枪械类型，0步枪，1轻型手枪，2重型手枪,切换武器会改变动画
    public int GunType
    {
        get { return gunType; }
    }
    //武器射击模式，0单发1全自动2爆发开火3全自动爆发
    private int gunShotMod;
    //枪械控制变量
    private int ammo;
    private float maxCoolTimeShot;
    private float coolTimeShot;
    private float reloadTime;
    private bool GunCooling;

    //数据管理
    [SerializeField]
    SuviorData suviorData;
    public GameObject TestPistol;
    public GameObject TestRifle;

    //用于生命值管理
    [SerializeField]
    DmgObj dmgObj;

    //输入控制
    public Vector2 inputVector;
    public bool AimCtrl;
    public bool ChangeWeaponCtrl;
    public bool RunCtrl;
    public Vector2 aimDifference;
    public bool halfAutoShotCtrl;
    public bool fullAutoShotCtrl;
    public bool reloadCtrl;
    //输入控制
    public enum SuviorAnimState
    {
        standing,
        walking,
        running,
        aimingGun,
        aimingHeavyPistol,
        aimingLightPistol

    }
    public enum FootPosState
    {
        normal,
        rifle,
    }
    private FootPosState footState;

    public FootPosState FootState
    {
        set { footState = value; }
    }


    private SuviorAnimState suviorState;

    public SuviorAnimState SuviorState
    {
        get { return suviorState; }
    }

    private void Start()
    {
        InsCtrlParas();
        isAiming = false;
        suviorState = SuviorAnimState.standing;

        suviorData.EquipGun(TestPistol);
        suviorData.EquipGun(TestRifle);
    }
    private void Update()
    {
        AimCtor();
        MoveCtor(2, inputVector);
        GunShotCtrl();
        GunShotTimer();
    }
    //射击计时器，用于决定武器是否允许射击
    private void GunShotTimer()
    {
        if (GunCooling == true && coolTimeShot > 0)
        {
            coolTimeShot -= Time.deltaTime;
        }
        else
        {
            GunCooling = false;
            coolTimeShot = maxCoolTimeShot;
        }
    }
    //武器射击控制,参数是爆发射击的连射数量
    private void GunShotCtrl()
    {
        if (isAiming == true && GunCooling == false && ammo > 0)
        {
            //Debug.Log("射击状态可用");
            //根据武器是否全自动决定射击方式
            if (halfAutoShotCtrl == true)
            {
                switch (gunShotMod)
                {
                    case 0:
                        Debug.Log("尝试半自动射击");
                        GunShotMethod();
                        GunCooling = true;
                        break;
                    case 2:
                        Debug.Log("尝试爆发射击");
                        StartCoroutine("GunBurstMethod");
                        GunCooling = true;
                        break;
                }
            }
            if (fullAutoShotCtrl == true)
            {
                switch (gunShotMod)
                {
                    case 1:
                        Debug.Log("尝试全自动射击");
                        GunShotMethod();
                        GunCooling = true;
                        break;
                    case 3:
                        Debug.Log("尝试爆发扫射");
                        StartCoroutine("GunBurstMethod");
                        GunCooling = true;
                        break;
                }
            }
        }
    }
    /// <summary>
    /// 武器射击方法
    /// </summary>
    private void GunShotMethod()
    {
        Debug.Log("枪械开火");
        for (int i = 0; i < suviorData.GunInUse.BulletCount; i++)
        {
            GameObject bullet = GameObject.Instantiate(suviorData.GunInUse.BulletPrefab, currentShotPoint.position, currentShotPoint.rotation);
            bullet.GetComponent<Bullet>().Damage = suviorData.GunInUse.Damage;
            bullet.GetComponent<Bullet>().Pierce = suviorData.GunInUse.PierceCount;
            Vector2 v = TransLocalToWorld(new Vector2(0, suviorData.GunInUse.BulletSpeed), currentShotPoint.gameObject);
            v = new Vector2(v.x + Random.Range(-suviorData.GunInUse.recoil, suviorData.GunInUse.recoil), v.y + Random.Range(-suviorData.GunInUse.recoil, suviorData.GunInUse.recoil));
            bullet.GetComponent<Rigidbody2D>().velocity = v;
        }
        ammo--;
    }
    /// <summary>
    /// 爆发射击需要协程实现
    /// </summary>
    /// <returns></returns>
    IEnumerator GunBurstMethod()
    {
        for (int i = 0; i < suviorData.GunInUse.BurstNum; i++)
        {
            GunShotMethod();
            yield return new WaitForSeconds(suviorData.GunInUse.BurstCool);
        }
    }
    private void AimCtor()
    {
        //鼠标右键瞄准
        if (AimCtrl == true && suviorData.GunInUse != null)
        {
            suviorData.GunInUse.transform.position = weaponFollowTrans.position;
            suviorData.GunInUse.transform.rotation = weaponFollowTrans.rotation;
            if (suviorData.GunInUse.ShowImage == false)
            {
                suviorData.GunInUse.ActiveWeaponSprite();
            }
            isAiming = true;
            ChangeSuviorState(SuviorAnimState.aimingGun);
            //角色指向鼠标方向
            SuviorSpriteObj.up = aimDifference;
            Foot.transform.up = Body.transform.up;

        }
        else
        {
            isAiming = false;
            if (suviorData.GunInUse != null && suviorData.GunInUse.ShowImage == true)
            {
                suviorData.GunInUse.ActiveWeaponSprite();
            }
            //角色指向鼠标方向
            SuviorSpriteObj.up = aimDifference;
            Foot.transform.up = SuviorSpriteObj.up;
        }
    }

    private void MoveCtor(float moveSpeed, Vector2 InputVector)
    {


        if (isAiming == true)
        {
            rigidbody2D.velocity = moveSpeed * inputVector * 0.3f;
            suviroAnimCtrl.footAnim.SetBool("Running", false);
            if (inputVector != Vector2.zero)
            {
                suviroAnimCtrl.footAnim.SetBool("Moving", true);
            }
            else
            {
                suviroAnimCtrl.footAnim.SetBool("Moving", false);
            }
        }
        else
        {
            if (RunCtrl == true && (inputVector != Vector2.zero))
            {
                ChangeSuviorState(SuviorAnimState.running);
                suviroAnimCtrl.footAnim.SetBool("Running", true);
                suviroAnimCtrl.footAnim.SetBool("Moving", true);
                rigidbody2D.velocity = moveSpeed * inputVector * 1.7f;
            }
            else if (inputVector != Vector2.zero)
            {
                ChangeSuviorState(SuviorAnimState.walking);
                rigidbody2D.velocity = moveSpeed * inputVector;
                suviroAnimCtrl.footAnim.SetBool("Running", false);
                suviroAnimCtrl.footAnim.SetBool("Moving", true);

            }
            else
            {
                ChangeSuviorState(SuviorAnimState.standing);
                rigidbody2D.velocity = Vector2.zero;
                suviroAnimCtrl.footAnim.SetBool("Running", false);
                suviroAnimCtrl.footAnim.SetBool("Moving", false);
            }

        }

    }

    private void ChangeSuviorState(SuviorAnimState state)
    {
        suviorState = state;
        suviroAnimCtrl.ChangeSpriteState(state);
    }



    /// <summary>
    /// 在已经装备两件武器时可以通过这个方法切换武器
    /// </summary>
    public void ChangeInUseWeapon()
    {
        if (suviorData.MainWeapon != null && suviorData.SideWeapon != null)
        {
            if (suviorData.GunInUse.MainOrNot == true)
            {
                //Debug.Log("开始切换到副武器");
                ChangeWeapon(false);
            }
            else
            {
                //Debug.Log("开始切换到主武器");
                ChangeWeapon(true);
            }
        }
        ChangeSuviorState(SuviorAnimState.aimingGun);
    }
    /// <summary>
    /// 在已经装备的武器之间切换
    /// </summary>
    /// <param name="mainOrSide"></param>
    public void ChangeWeapon(bool mainOrSide)
    {
        //如果该武器已经装备到背包就采用武器的参数，并且使用背包中记录的武器弹药数量
        if (suviorData.MainWeapon != null && mainOrSide == true)
        {
            if (suviorData.GunInUse != null && suviorData.GunInUse.ShowImage == true)
            {
                suviorData.GunInUse.ActiveWeaponSprite();
            }
            Debug.Log("启用了主武器");
            suviorData.sideWeaponAmmo = ammo;
            suviorData.GunInUse = suviorData.MainWeapon;
            ammo = suviorData.mainWeaponAmmo;
            SetGunValue();
        }
        else if (suviorData.SideWeapon != null && mainOrSide == false)
        {
            if (suviorData.GunInUse != null && suviorData.GunInUse.ShowImage == true)
            {
                suviorData.GunInUse.ActiveWeaponSprite();
            }
            Debug.Log("启用了副武器");
            suviorData.mainWeaponAmmo = ammo;
            suviorData.GunInUse = suviorData.SideWeapon;
            ammo = suviorData.sideWeaponAmmo;
            SetGunValue();
        }
    }

    private void SetGunValue()
    {
        gunType = suviorData.GunInUse.ArtType;
        switch (suviorData.GunInUse.ArtType)
        {
            case 0:
                weaponFollowTrans = rifleTrans;
                break;
            case 1:
                weaponFollowTrans = lightPistolTrans;
                break;
            case 2:
                weaponFollowTrans = heavyPistolTrans;
                break;
        }
        currentShotPoint = suviorData.GunInUse.ShotPos;
        gunShotMod = suviorData.GunInUse.ShotMod;
        maxCoolTimeShot = suviorData.GunInUse.CoolTime;
        GunCooling = false;
        ammo = suviorData.GunInUse.MaxAmmo;
        Debug.Log(ammo);
    }

    //基于本地向量转基于世界坐标的向量
    public static Vector2 TransLocalToWorld(Vector2 v, GameObject gameObject)
    {
        if (v.x == 0)
        {
            float angel1 = gameObject.GetComponent<Transform>().eulerAngles.z;
            float sinA1 = Mathf.Sin(angel1 * (Mathf.PI / 180));
            float cosA1 = Mathf.Cos(angel1 * (Mathf.PI / 180));
            float a = -sinA1 * v.y;
            float b = cosA1 * v.y;
            Vector2 returnV = new Vector2(a, b);
            return returnV;
        }
        else
        {
            //第一步，获得角色当前的旋转角，获得这个旋转角对应的sin和cos值
            float angel = gameObject.GetComponent<Transform>().eulerAngles.z;
            float sinA = Mathf.Sin(angel * (Mathf.PI / 180));
            float cosA = Mathf.Cos(angel * (Mathf.PI / 180));
            float y1 = v.y * cosA;
            float x1 = v.y * sinA;
            float y2 = v.x * sinA;
            float x2 = v.x * cosA;
            Vector2 returnV = new Vector2(x1 + x2, y1 + y2);
            return returnV;
        }
    }
    //初始化控制输入变量
    public void InsCtrlParas()
    {
        inputVector = Vector2.zero;
        AimCtrl = false;
        RunCtrl = false;
        ChangeWeaponCtrl = false;
        aimDifference = Vector2.zero;
        halfAutoShotCtrl = false;
        fullAutoShotCtrl = false;
        reloadCtrl = false;
    }
}
