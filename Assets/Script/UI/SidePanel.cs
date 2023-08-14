using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SidePanel : MonoBehaviour
{
    public Image BG;
    public Text Title;
    public Image Img;
    public Image ImgBg;
    public GameObject Attributes;
    public Text Describe;
    public Sprite defaultImg;
    /// <summary>
    /// 描述Ui初始化
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="item"></param>
    public void SidePanelIni(int mode,item item)
    {
        switch (mode)
        {
            case 0:
                BG.color = new Color(0.5f, 0.5f, 0.5f, 1);
                Title.text = null;
                Img.color = new Color(0, 0, 0, 0);
                ImgBg.color = new Color(0, 0, 0, 0);
                Attributes.SetActive(false);
                Describe.text = null;
                break;
            case 1:
                BG.color =  new Color(1, 0.8f, 0, 1);
                Title.text = "该栏位无道具";
                Img.color = new Color(0, 0, 0, 0);
                ImgBg.color = new Color(0, 0, 0, 0);
                Attributes.SetActive(false);
                Describe.text = null;
                break;
            case 2:
                BG.color = new Color(1, 0.8f, 0, 1);
                Title.text =item.Name;
                Img.color = new Color(1, 1, 1, 1);
                ImgBg.color = new Color(0.5f, 0.5f, 0.5f, 1);
                if (item.ImageInPack == null)
                {
                    Img.sprite = defaultImg;
                }
                else
                {
                    Img.sprite = item.ImageInPack;
                }
                Attributes.SetActive(false);
                Describe.text = null;
                break;
            default:
                break;
        }
    }

}
