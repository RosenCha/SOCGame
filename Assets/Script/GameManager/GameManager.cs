using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool pausing;
    public bool Pausing
    {
        get { return pausing; }
    }

    private void Start()
    {
        Cursor.visible = false;
    }
    public UI ui;
    public SuviorData data;

    /// <summary>
    /// ��ͣ��ȡ����Ϸ��ͣ
    /// </summary>
    /// <param name="PauseOrNot">�л�����ͣ�����л�������</param>
    public void PauseGame(bool PauseOrNot)
    {
        switch (PauseOrNot)
        {
            case true:
                if (pausing == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.lockState = CursorLockMode.None;
                    pausing = true;
                    Time.timeScale = 0;
                    ui.ShowPanel(true);
                }
                break;
            case false:
                if (pausing == true)
                {
                    Cursor.visible = false;

                    pausing = false;
                    Time.timeScale = 1;
                    ui.ShowPanel(false);
                }
                break;
        }
    }

}
