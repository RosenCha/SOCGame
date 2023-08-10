using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 这个脚本用于控制角色动画
/// </summary>
public class SuviroAnimController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public Animator footAnim;
    [SerializeField]
    public SpriteRenderer head;
    [SerializeField]
    public SpriteRenderer body;
    [SerializeField]
    public SpriteRenderer leftArm;
    [SerializeField]
    public SpriteRenderer leftHand;
    [SerializeField]
    public SpriteRenderer rightArm;
    [SerializeField]
    public SpriteRenderer rightHand;
    [SerializeField]
    GameObject rifle;
    [SerializeField]
    GameObject heavyPistol;
    [SerializeField]
    GameObject lightPistol;
    [SerializeField]
    public SpriteRenderer bagSpriteRender;
    [SerializeField]
    public SpriteRenderer outerSpriterRender;

    public Sprite None;
    public Sprite headS;
    public Sprite bodyS;
    public Sprite leftArmS;
    public Sprite leftHandS;
    public Sprite rightArmS;
    public Sprite rightHandS;

    public Sprite bodyS_Basic;
    public Sprite leftArmS_Basic;
    public Sprite leftHandS_Basic;
    public Sprite rightArmS_Basic;
    public Sprite rightHandS_Basic;
    public Sprite bag_Basic;
    public Sprite clouth_Basic;
    public Sprite outer_Basic;
    [SerializeField]
    SuviorCtrl suviorCtrl;
    public Transform footTrans;
    public void ChangeSpriteState(SuviorCtrl.SuviorAnimState state)
    {
        switch (state)
        {
            case SuviorCtrl.SuviorAnimState.standing:
                animator.SetBool("Aiming", false);
                animator.SetBool("Moving", false);
                animator.SetBool("Running", false);
                leftArm.sprite = None;
                rightArm.sprite = None;
                head.sprite = headS;
                body.sprite = bodyS;
                leftHand.sprite = leftHandS;
                rightHand.sprite = rightHandS;
                break;
            case SuviorCtrl.SuviorAnimState.walking:
                animator.SetBool("Aiming", false);
                animator.SetBool("Moving", true);
                animator.SetBool("Running", false);
                leftArm.sprite = None;
                rightArm.sprite = None;
                head.sprite = headS;
                body.sprite = bodyS;
                leftHand.sprite = leftHandS;
                rightHand.sprite = rightHandS;
                break;
            case SuviorCtrl.SuviorAnimState.running:
                animator.SetBool("Aiming", false);
                animator.SetBool("Moving", true);
                animator.SetBool("Running", true);
                leftArm.sprite = leftArmS;
                rightArm.sprite = rightArmS;
                head.sprite = headS;
                body.sprite = bodyS;
                leftHand.sprite = leftHandS;
                rightHand.sprite = rightHandS;
                break;
            case SuviorCtrl.SuviorAnimState.aimingGun:
                animator.SetBool("Aiming", true);
                animator.SetBool("Moving", false);
                animator.SetBool("Running", false);
                animator.SetInteger("GunType", suviorCtrl.GunType);
                leftArm.sprite = leftArmS;
                rightArm.sprite = rightArmS;
                head.sprite = headS;
                body.sprite = bodyS;
                leftHand.sprite = leftHandS;
                rightHand.sprite = rightHandS;
                break;
            case SuviorCtrl.SuviorAnimState.aimingHeavyPistol:
                break;
            case SuviorCtrl.SuviorAnimState.aimingLightPistol:
                break;
            default:
                break;
        }
    }

    public void ChangeFootState(SuviorCtrl.FootPosState state)
    {
        switch (state)
        {
            case SuviorCtrl.FootPosState.normal:
                footTrans.rotation = Quaternion.Euler(Vector3.zero);
                break;
            case SuviorCtrl.FootPosState.rifle:
                footTrans.rotation = Quaternion.Euler(new Vector3(0,0,-29.5f));
                break;
            default:
                break;
        }
    }
}
