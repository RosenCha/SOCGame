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
    /// 暂停或取消游戏暂停
    /// </summary>
    /// <param name="PauseOrNot">切换到暂停还是切换到继续</param>
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
