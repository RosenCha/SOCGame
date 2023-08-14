using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҿ����������︺�����룬���õ���Suvior�ķ���
/// </summary>
public class playerController : MonoBehaviour
{
    public SuviorCtrl InCtrlSuvior;
    public GameManager gm;
    // Update is called once per frame

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }
    void Update()
    {
        if (gm.Pausing == false)
        {
            MoveCtrl();
            AimCtrl();
        }
        UiCtrl();
    }

    private void UiCtrl()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gm.Pausing == false)
            {
                gm.PauseGame(true);
                
            }
            else
            {
                gm.PauseGame(false);
            }
        }
    }
    private void AimCtrl()
    {
        InCtrlSuvior.aimDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Input.GetMouseButton(1))
        {
            InCtrlSuvior.AimCtrl = true;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                InCtrlSuvior.ChangeInUseWeapon();
            }
            if (Input.GetMouseButtonDown(0))
            {
                InCtrlSuvior.halfAutoShotCtrl = true;
            }
            else
            {
                InCtrlSuvior.halfAutoShotCtrl = false;
            }
            if (Input.GetMouseButton(0))
            {
                InCtrlSuvior.fullAutoShotCtrl = true;
            }
            else
            {
                InCtrlSuvior.fullAutoShotCtrl = false;
            }
        }
        else
        {
            InCtrlSuvior.AimCtrl = false;
        }

    }
    private void MoveCtrl()
    {
        // ��ȡ��������ˮƽ�ʹ�ֱ����
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ���������������ڵ�λ������
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;
        InCtrlSuvior.inputVector = inputVector;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            InCtrlSuvior.RunCtrl = true;
        }
        else
        {
            InCtrlSuvior.RunCtrl = false;
        }

    }
}
