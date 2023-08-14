using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipMentBlank : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameManager gm;
    bool EmptyOrNot;
    bool ChosenOrNot;
    public Image BG;
    public Image Front;
    public Text text;
    public GameObject Space;
    public GameObject ItemBlankPrefab;
    public Image ItemImg;
    public Sprite defaultImg;
    item InBlankItem;
    item_Equipment InBlankEquipment;
    //后面这项仅用于普通栏位
    public bool EquipmentBlankOrNormalBlank;
    public storageCell InBlankCell;
    public enum type
    {
        主武器,
        副武器,
        近战,
        快捷,
        背包,
        服装,
        外套,
    }
    public type Type;
    /// <summary>
    /// 改变外形
    /// </summary>
    public void  ChangeToEmpty()
    {
        if (ChosenOrNot == true)
        {
            BG.color = new Color(1, 0.8f, 0, 1);
        }
        else
        {
            if (EmptyOrNot == true)
            {
                Front.color = new Color(0.15f, 0.15f, 0.15f, 1);

                BG.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                BG.color = new Color(0.5f, 0.5f, 0.5f, 1);
                Front.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }
    }
    public void Chose()
    {
        if (ChosenOrNot == false)
        {
            ChoseBlank(true);
        }
        else
        {
            ChoseBlank(false);
        }
    }

    public void ChoseBlank(bool choseeOrNot)
    {
        if (choseeOrNot == true)
        {
            gm.ui.BackPackPanel.ReChose();
            gm.ui.ChosenBlank = this;
            ChosenOrNot = true;
            if (EmptyOrNot == true)
            {
                gm.ui.SidePanelIni(1, null);

            }
            else
            {
                gm.ui.SidePanelIni(2, InBlankItem);
            }
            OnEnter();
        }
        else
        {
            ChosenOrNot = false;
            gm.ui.SidePanelIni(0, null);
        }
        ChangeToEmpty();

    }
    public virtual void IniEquipMentBlank()
    {
        InBlankEquipment = null;
        InBlankItem = null;
        if (EquipmentBlankOrNormalBlank == true)
        {
            ItemImg.color = new Color(0, 0, 0, 0);
            if (Space != null)
            {
                if (Space.transform.childCount != 0)
                {
                    EquipMentBlank[] a = Space.transform.GetComponentsInChildren<EquipMentBlank>();
                    foreach (var item in a)
                    {
                        Destroy(item.gameObject);
                    }
                }
            }
            EmptyOrNot = true;
            ChosenOrNot = false;
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
            switch (Type)
            {
                case type.主武器:
                    break;
                case type.副武器:
                    break;
                case type.近战:
                    break;
                case type.快捷:
                    break;
                case type.背包:
                    if (gm.data.BackPack != null)
                    {
                        EmptyOrNot = false;

                        InBlankItem = gm.data.BackPack;
                        InBlankEquipment = gm.data.BackPack;
                    }
                    else
                    {
                        EmptyOrNot = true;
                    }
                    IniImage(InBlankItem);
                    break;
                case type.服装:
                    if (gm.data.Clouth != null)
                    {
                        EmptyOrNot = false;
                        InBlankItem = gm.data.Clouth;
                        InBlankEquipment = gm.data.Clouth;
                    }
                    else
                    {
                        EmptyOrNot = true;
                    }
                    IniImage(InBlankItem);
                    break;
                case type.外套:
                    if (gm.data.Outer != null)
                    {
                        EmptyOrNot = false;
                        InBlankItem = gm.data.Outer;
                        InBlankEquipment = gm.data.Outer;
                    }
                    else
                    {
                        EmptyOrNot = true;
                    }
                    IniImage(InBlankItem);
                    break;
                default:
                    break;
            }
            InstantiateChildBlanks();
            ChangeToEmpty();
        }
    }
    public void InstantiateChildBlanks()
    {
        if (InBlankEquipment != null&&InBlankEquipment.SpaceCount>0)
        {
            Debug.Log("生成");
            for (int i = 0; i < InBlankEquipment.SpaceCount ; i++)
            {
                GameObject o = Instantiate(ItemBlankPrefab);
                o.transform.SetParent(Space.transform);
                o.GetComponent<EquipMentBlank>().IniItemBlank(InBlankEquipment.StorageSpace[i]);
              
            }
        }
    }
    public void IniItemBlank(storageCell cell)
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        InBlankCell = cell;
        if (cell.ItemInCell == null)
        {
            text.text = null;
            ItemImg.color = new Color(0, 0, 0, 0);
            EmptyOrNot = true;
            ChosenOrNot = false;
        }
        else
        {
            InBlankItem = cell.ItemInCell;
            ItemImg.color = new Color(1, 1, 1, 1);
            if (cell.ItemInCell.ImageInPack == null)
            {
                ItemImg.sprite = cell.ItemInCell.ImageInPack;
            }
            else
            {
                ItemImg.sprite = defaultImg;
            }
            EmptyOrNot = false;
            ChosenOrNot = false;
            if (cell.ItemInCell.stackAble == false)
            {
                text.text = null;
            }
            else
            {
                text.text = cell.Count.ToString();
            }
        }
        ChangeToEmpty();
        IniImage(InBlankItem);
    }
    public void IniImage(item item)
    {
        if (item == null)
        {
            ItemImg.color = new Color(1, 1, 1, 0);
        }
        else
        {
            if (item.ImageInPack != null)
            {
                ItemImg.sprite = item.ImageInPack;
                ItemImg.color = new Color(1, 1, 1, 1);

            }
            else

            {
                ItemImg.sprite = defaultImg;
                ItemImg.color = new Color(1, 1, 1, 1);

            }
        }

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        BG.color = new Color(0.23f,1,0,1);
        OnEnter();


    }
    public void OnEnter()
    {
        if (EquipmentBlankOrNormalBlank == true)
        {
            if (ChosenOrNot == true)
            {
                if (InBlankItem == null)
                {
                    gm.ui.TypeTip(null);
                }
                else
                {
                    gm.ui.TypeTip("鼠标右键卸下装备");
                }
            }
            else
            {
                if (InBlankItem != null)
                {
                    gm.ui.TypeTip("鼠标左键选中该栏位");
                }

            }

        }
        else
        {
            if (InBlankItem == null)
            {
                gm.ui.TypeTip(null);
            }
            else
            {
                if (ChosenOrNot == true)
                {
                    gm.ui.TypeTip("右键本栏位使用道具,右键空栏位转移道具");
                }
                else
                {
                    gm.ui.TypeTip("鼠标左键选中该栏位");
                }
            }
        }

    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        ChangeToEmpty();
        gm.ui.TypeTip(null);
    }

    //右键点击为"使用"
    public void ClickBlank(bool leftOrNot)
    {
        //左键
        if (leftOrNot == true) {
            Chose();
        }
        //右键
        else
        {

            //是装备栏位
            if (EquipmentBlankOrNormalBlank == true)
            {
                if (ChosenOrNot == true)
                {
                    if (InBlankItem != null)
                    {
                        if (gm.data.TryUnEquipE(InBlankItem) == true)
                        {
                            gm.ui.IniUiPanel();

                        }
                        else
                        {
                            gm.ui.TypeTip("没有足够的空间卸下这件装备和其中存放的道具");
                        }
                    }
                }
            }
            //是道具栏位
            else
            {
                if (InBlankItem != null)
                {
                    if((ChosenOrNot==false&&gm.ui.ChosenBlank != null && gm.ui.ChosenBlank.InBlankCell.ItemInCell.stackAble == true)){
                        if (gm.ui.ChosenBlank.InBlankItem.ID == InBlankItem.ID && InBlankCell.Count<InBlankItem.stackCount) { 
                           if((gm.ui.ChosenBlank.InBlankCell.Count+InBlankCell.Count)> InBlankItem.stackCount)
                            {
                                gm.ui.ChosenBlank.InBlankCell.TryGetItem(InBlankItem.stackCount-InBlankCell.Count);
                                InBlankCell.TryStorgeItem(InBlankItem, InBlankItem.stackCount - InBlankCell.Count);
                                gm.ui.IniUiPanel();
                            }
                            else
                            {
                                InBlankCell.TryStorgeItem(InBlankItem, gm.ui.ChosenBlank.InBlankCell.Count);
                                gm.ui.ChosenBlank.InBlankCell.CleanCell();
                                gm.ui.IniUiPanel();
                            }
                        
                        }

                    }
                    else if(gm.ui.ChosenBlank != null&&gm.ui.ChosenBlank.InBlankItem.ID != InBlankItem.ID)
                    {
                        gm.ui.TypeTip("不同的道具不能堆叠");
                    }
                    else
                    {
                        //右键装备装备
                        switch (InBlankItem.itemType)
                        {
                            case item.ItemType.emptyObject0:
                                break;
                            case item.ItemType.meleeWeapon1:
                                break;
                            case item.ItemType.gun2:
                                break;
                            case item.ItemType.ammo3:
                                break;
                            case item.ItemType.bag4:
                                if (gm.data.BackPack == null)
                                {
                                    gm.data.EquipE(InBlankItem.gameObject.GetComponent<item_Equipment>());
                                    InBlankCell.CleanCell();
                                    gm.ui.IniUiPanel();

                                }
                                else
                                {
                                    gm.ui.TypeTip("必须先卸下对应栏位的装备");
                                }
                                break;
                            case item.ItemType.clothing5:
                                if (gm.data.Clouth == null)
                                {
                                    gm.data.EquipE(InBlankItem.gameObject.GetComponent<item_Equipment>());
                                    InBlankCell.CleanCell();
                                    gm.ui.IniUiPanel();
                                }
                                else
                                {
                                    gm.ui.TypeTip("必须先卸下对应栏位的装备");
                                }
                                break;
                            case item.ItemType.coat6:
                                if (gm.data.Outer == null)
                                {
                                    gm.data.EquipE(InBlankItem.gameObject.GetComponent<item_Equipment>());
                                    InBlankCell.CleanCell();
                                    gm.ui.IniUiPanel();
                                }
                                else
                                {
                                    gm.ui.TypeTip("必须先卸下对应栏位的装备");
                                }
                                break;
                            case item.ItemType.food7:
                                break;
                            case item.ItemType.med8:
                                break;
                            case item.ItemType.tool9:
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    if (gm.ui.ChosenBlank != null&& gm.ui.ChosenBlank.EquipmentBlankOrNormalBlank==false)
                    {
                        gm.ui.ChosenBlank.InBlankCell.TransToAnotherCell(this.InBlankCell);
                        gm.ui.IniUiPanel();
                    }else if (gm.ui.ChosenBlank.EquipmentBlankOrNormalBlank == true)
                    {
                        gm.ui.TypeTip("必须先卸下装备才能转移存放位置");
                    }
                }
            }
        }

    }
}
