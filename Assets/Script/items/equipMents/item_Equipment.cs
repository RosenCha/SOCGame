using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����,�·�,��ҵĸ���,�����ṩһ���������洢��Ʒ
/// </summary>
public class item_Equipment : item
{
    public int SpaceCount;
    public int HpAdd;
    public int DefenseAdd;

    
    public List<storageCell> StorageSpace;

    /// <summary>
    /// װ����ʼ��,����װ�����½�������׼���洢��Ʒ
    /// </summary>
    public void IniEquipMent()
    {
        StorageSpace = new List<storageCell>();
        //Debug.Log("������" + SpaceCount + "������");
        
        for (int i = 0; i < SpaceCount; i++)
        {
            //�½�����
            StorageSpace.Add(new storageCell());
            StorageSpace[i].CleanCell();
        }
    }

    /// <summary>
    /// ���Դ洢����,����ֵ��ʾ�ж��ٸ�����û�Ž�ȥ
    /// </summary>
    /// <param name="item">Ҫ�������Ʒ</param>
    /// <param name="count">Ҫ���������</param>
    /// <returns></returns>
    public int TryStorageItem(item item,int count)
    {
        //����ÿ������
        for (int i = 0; i < SpaceCount; i++)
        {
            //��������Ѿ����ڶ���,�ж��ܷ�ѵ�
            if (StorageSpace[i].Count>0 && StorageSpace[i].ItemInCell.ID == item.ID && item.stackAble == true)
            {
                //������Զѵ����Ҷѵ��������������޾�ֱ�Ӷѵ�
                if(count+ StorageSpace[i].Count <= item.stackCount)
                {
                    StorageSpace[i].TryStorgeItem(item,count);
                    return 0;
                }
                //����ѵ����������ѵ�����
                else
                {
                    //�Ȱѵ�ǰ���Ӵ����ټ�����������ĸ����ܲ��ܴ��¶�������
                    int difference = item.stackCount - StorageSpace[i].Count;
                    StorageSpace[i].TryStorgeItem(item, difference);
                    count -= difference;
                    continue;
                }
            //������ܶѵ�,�жϸ����Ƿ��ǿյ�,����Ǿ�ֱ�Ӵ���
            }else if (StorageSpace[i].Count == 0)
            {
                //�ȿ���Ҫ��Ķ����ܷ�ѵ�
                if (item.stackAble == true)
                {
                    //��������������ѵ����޾�ֱ�Ӵ���
                    if (count <= item.stackCount)
                    {
                        StorageSpace[i].TryStorgeItem(item, count);
                        return 0;
                    }
                    //��������ѵ������˾ʹ���������Ӳ�����������һ������
                    else
                    {
                        StorageSpace[i].TryStorgeItem(item,item.stackCount);
                        count -= item.stackCount;
                        continue;
                    }
                }
                //������ܶѵ���ֱ�Ӵ���ո���
                else
                {
                    StorageSpace[i].TryStorgeItem(item, count);
                    return 0;
                }
            }
            //������Ӳ����Ҳ��ܶѵ�,����������һ������
            else
            {
                continue;
            }
        }
        //���forѭ��������û����Ȼ���ؾ�˵��û�пռ���,����û�ܴ��������
        return count;
    }

    /// <summary>
    /// ����ʹ��IDȡ����Ʒ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    /// <returns>ȡ��������</returns>
    public int TryGetItemThroughID(string id,int count)
    {
        //���������ʾ�ɹ�ȡ�������ٸ�
        int returnCount=0;
        //����ÿ������
        for (int i = 0; i < stackCount; i++)
        {
            //������������ô���Ҫȡ������Ʒ
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
