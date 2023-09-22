using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    [SerializeField]
    GameManager gm;

    public GameObject PausePanel;
    public GameObject PagePanel;
    public GameObject FunctionPanel;
    public GameObject SidePanel;
    public BackPackPanel BackPackPanel;
    public Text TipText;
    public EquipMentBlank ChosenBlank;
   public void SidePanelIni(int i,item item)
    {
        GetComponentInChildren<SidePanel>().SidePanelIni(i, item);
    }
    public void ClickPause()
    {
        gm.PauseGame(false);
        ShowPanel(false);
    }
    public void ShowPanel(bool ShowOrClose)
    {
        if (ShowOrClose == true)
        {
            IniUiPanel();
        }
        else
        {
            PausePanel.SetActive(false);
            FunctionPanel.SetActive(false);
            SidePanel.SetActive(false);
            PagePanel.SetActive(false);
            TipText.gameObject.SetActive(false);
        }
    }
    public void IniUiPanel()
    {
        TipText.gameObject.SetActive(true);
        PausePanel.SetActive(true);
        FunctionPanel.SetActive(true);
        SidePanel.SetActive(true);
        PagePanel.SetActive(true);
        GetComponentInChildren<BackPackPanel>().IniBackPackPanel();
        buttonEffect[] b = GetComponentsInChildren<buttonEffect>();
        foreach (var item in b)
        {
            item.IniButton();
        }

        GetComponentInChildren<SidePanel>().SidePanelIni(0,null);
        TypeTip(null);
        ChosenBlank = null;
    }
    public void TypeTip(string str)
    {
        TipText.text = str;
    }
}
