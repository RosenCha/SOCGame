using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player controller,which manage  the charactor logic 
public class SuviorCtrl : MonoBehaviour
{
    [SerializeField]
    //��ɫ����
    Transform PlayerTrans;
    [SerializeField]
    //��ɫ������
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

    //���ʹ�õ�ǹе���ͣ�0��ǹ��1������ǹ��2������ǹ,�л�������ı䶯��
    public int GunType
    {
        get { return gunType; }
    }
    //�������ģʽ��0����1ȫ�Զ�2��������3ȫ�Զ�����
    private int gunShotMod;
    //ǹе���Ʊ���
    private int ammo;
    private float maxCoolTimeShot;
    private float coolTimeShot;
    private float reloadTime;
    private bool GunCooling;

    //���ݹ���
    [SerializeField]
    SuviorData suviorData;
    public GameObject TestPistol;
    public GameObject TestRifle;

    //��������ֵ����
    [SerializeField]
    DmgObj dmgObj;

    //�������
    public Vector2 inputVector;
    public bool AimCtrl;
    public bool ChangeWeaponCtrl;
    public bool RunCtrl;
    public Vector2 aimDifference;
    public bool halfAutoShotCtrl;
    public bool fullAutoShotCtrl;
    public bool reloadCtrl;
    //�������
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
    //�����ʱ�������ھ��������Ƿ��������
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
    //�����������,�����Ǳ����������������
    private void GunShotCtrl()
    {
        if (isAiming == true && GunCooling == false && ammo > 0)
        {
            //Debug.Log("���״̬����");
            //���������Ƿ�ȫ�Զ����������ʽ
            if (halfAutoShotCtrl == true)
            {
                switch (gunShotMod)
                {
                    case 0:
                        Debug.Log("���԰��Զ����");
                        GunShotMethod();
                        GunCooling = true;
                        break;
                    case 2:
                        Debug.Log("���Ա������");
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
                        Debug.Log("����ȫ�Զ����");
                        GunShotMethod();
                        GunCooling = true;
                        break;
                    case 3:
                        Debug.Log("���Ա���ɨ��");
                        StartCoroutine("GunBurstMethod");
                        GunCooling = true;
                        break;
                }
            }
        }
    }
    /// <summary>
    /// �����������
    /// </summary>
    private void GunShotMethod()
    {
        Debug.Log("ǹе����");
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
    /// ���������ҪЭ��ʵ��
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
        //����Ҽ���׼
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
            //��ɫָ����귽��
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
            //��ɫָ����귽��
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
    /// ���Ѿ�װ����������ʱ����ͨ����������л�����
    /// </summary>
    public void ChangeInUseWeapon()
    {
        if (suviorData.MainWeapon != null && suviorData.SideWeapon != null)
        {
            if (suviorData.GunInUse.MainOrNot == true)
            {
                //Debug.Log("��ʼ�л���������");
                ChangeWeapon(false);
            }
            else
            {
                //Debug.Log("��ʼ�л���������");
                ChangeWeapon(true);
            }
        }
        ChangeSuviorState(SuviorAnimState.aimingGun);
    }
    /// <summary>
    /// ���Ѿ�װ��������֮���л�
    /// </summary>
    /// <param name="mainOrSide"></param>
    public void ChangeWeapon(bool mainOrSide)
    {
        //����������Ѿ�װ���������Ͳ��������Ĳ���������ʹ�ñ����м�¼��������ҩ����
        if (suviorData.MainWeapon != null && mainOrSide == true)
        {
            if (suviorData.GunInUse != null && suviorData.GunInUse.ShowImage == true)
            {
                suviorData.GunInUse.ActiveWeaponSprite();
            }
            Debug.Log("������������");
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
            Debug.Log("�����˸�����");
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

    //���ڱ�������ת�����������������
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
            //��һ������ý�ɫ��ǰ����ת�ǣ���������ת�Ƕ�Ӧ��sin��cosֵ
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
    //��ʼ�������������
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
