using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 储物空间类
/// </summary>
public class storageCell : MonoBehaviour
{
    private item itemInCell;
    public item ItemInCell
    {
        get { return itemInCell; }
    }
    private int count;
    public int Count
    {
        get { return count; }
    }
    
    /// <summary>
    /// 放入物体,返回true表示放置成功返回False表示放置失败
    /// </summary>
    /// <param name="item">要放入的物体</param>
    /// <param name="num">数量</param>
    public bool TryStorgeItem(item item,int num)
    {
        //第一步自己有没有物品并判断该物品是否可堆叠,如果自己没有物品直接继续.
        if (itemInCell!=null)
        {
            //如果有物品,判断传入的物品和本身存储的物品是否可以堆叠,堆叠数量则交给外部判断
            if (itemInCell.stackAble==true && item.ID == ItemInCell.ID)
            {
                //对数量进行堆叠
                count += num;
                return true;
            }
            else
            {
                return false;
            }
        }
        //如果自己没有存储物品,则直接存入就行了
        else
        {
            if (item.stackAble == true)
            {
                count += num;
            }
            else
            {
                count = 1;
            }
            itemInCell = item;
            return true;
        }
    }

    /// <summary>
    /// 尝试取出物品
    /// </summary>
    /// <param name="num"></param>
    /// <returns>取出的数量</returns>
    public int TryGetItem(int num)
    {
        if (count - num > 0)
        {
            count -= num;
            return num;
        }
        else
        {
            CleanCell();
            return count;
        }
    }
    /// <summary>
    /// 清空格子
    /// </summary>
    public void CleanCell() {
        //if (ItemInCell != null)
        //{
        //    Destroy(ItemInCell.gameObject);
        //}

        itemInCell = null;
        count = 0;
    }

    public void TransToAnotherCell(storageCell cell)
    {
        if (cell.count == 0)
        {
            cell.TryStorgeItem(itemInCell, count);
        }
        CleanCell();
    }
}
