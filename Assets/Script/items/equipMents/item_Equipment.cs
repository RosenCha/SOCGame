using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包,衣服,外挂的父类,可以提供一定格子来存储物品
/// </summary>
public class item_Equipment : item
{
    public int SpaceCount;
    public int HpAdd;
    public int DefenseAdd;

    
    public List<storageCell> StorageSpace;

    /// <summary>
    /// 装备初始化,会在装备中新建格子来准备存储物品
    /// </summary>
    public void IniEquipMent()
    {
        StorageSpace = new List<storageCell>();
        //Debug.Log("生成了" + SpaceCount + "个格子");
        
        for (int i = 0; i < SpaceCount; i++)
        {
            //新建格子
            StorageSpace.Add(new storageCell());
            StorageSpace[i].CleanCell();
        }
    }

    /// <summary>
    /// 尝试存储道具,返回值表示有多少个道具没放进去
    /// </summary>
    /// <param name="item">要存入的物品</param>
    /// <param name="count">要存入的数量</param>
    /// <returns></returns>
    public int TryStorageItem(item item,int count)
    {
        //遍历每个格子
        for (int i = 0; i < SpaceCount; i++)
        {
            //如果格子已经存在东西,判断能否堆叠
            if (StorageSpace[i].Count>0 && StorageSpace[i].ItemInCell.ID == item.ID && item.stackAble == true)
            {
                //如果可以堆叠并且堆叠数量不超过上限就直接堆叠
                if(count+ StorageSpace[i].Count <= item.stackCount)
                {
                    StorageSpace[i].TryStorgeItem(item,count);
                    return 0;
                }
                //如果堆叠数量超过堆叠上限
                else
                {
                    //先把当前格子存满再继续遍历后面的格子能不能存下多余数量
                    int difference = item.stackCount - StorageSpace[i].Count;
                    StorageSpace[i].TryStorgeItem(item, difference);
                    count -= difference;
                    continue;
                }
            //如果不能堆叠,判断格子是否是空的,如果是就直接存入
            }else if (StorageSpace[i].Count == 0)
            {
                //先看看要存的东西能否堆叠
                if (item.stackAble == true)
                {
                    //如果数量不超过堆叠上限就直接存入
                    if (count <= item.stackCount)
                    {
                        StorageSpace[i].TryStorgeItem(item, count);
                        return 0;
                    }
                    //如果超过堆叠上限了就存满这个格子并继续遍历下一个格子
                    else
                    {
                        StorageSpace[i].TryStorgeItem(item,item.stackCount);
                        count -= item.stackCount;
                        continue;
                    }
                }
                //如果不能堆叠就直接存入空格子
                else
                {
                    StorageSpace[i].TryStorgeItem(item, count);
                    return 0;
                }
            }
            //如果格子不空且不能堆叠,继续遍历下一个格子
            else
            {
                continue;
            }
        }
        //如果for循环结束了没有自然返回就说明没有空间了,返回没能存入的数量
        return count;
    }

    /// <summary>
    /// 尝试使用ID取出物品
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    /// <returns>取出的数量</returns>
    public int TryGetItemThroughID(string id,int count)
    {
        //这个变量表示成功取出来多少个
        int returnCount=0;
        //遍历每个格子
        for (int i = 0; i < stackCount; i++)
        {
            //如果格子中正好存着要取出的物品
            if(StorageSpace[i].Count == 0 && StorageSpace[i].ItemInCell.ID == id)
            {
                returnCount += StorageSpace[i].TryGetItem(count-returnCount);
            }
            if (returnCount == count)
            {
                return returnCount;
            }
        }
        return returnCount;
    }

    
}
