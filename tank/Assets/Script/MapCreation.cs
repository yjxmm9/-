using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //��ʼ����ͼ������
    //0.�ϼ� 1.ǽ 2.�ϰ� 3.����Ч�� 4.���� 5.�� 6.����ǽ
    public GameObject[] item;


    //�Ѿ��ж�����λ���б�
    private List<Vector3> itemPositionList = new List<Vector3>();


    private void Awake()
    {
        InitMap();
    }

    private void InitMap()
    {
        //ʵ�����ϼ�
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //�ϼ�Χ����
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        //ʵ������Χ����ǽ
        for (int i = -16; i < 17; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -16; i < 17; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-16, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(16, i, 0), Quaternion.identity);
        }
        //ʵ�������
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;

        //ʵ��������
        CreateItem(item[3], new Vector3(-14, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(14, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);

        InvokeRepeating("CreateEnemy", 4, 5);
        //ʵ������ͼ
        for (int i = 0; i < 70; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 30; i++)
        {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 30; i++)
        {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 30; i++)
        {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
    }
    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createrotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createrotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    //�������λ�õķ���
    private Vector3 CreateRandomPosition()
    {
        //��������ΧһȦ
        while(true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-14, 15),Random.Range(-6,7),0);
            if(!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }

    //�����ж�λ���б����Ƿ������λ��
    private bool HasThePosition(Vector3 createPos)
    {
        for(int i=0;i<itemPositionList.Count;i++)
        {
            if(createPos==itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    //�������˵ķ���
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if(num==0)
        {
            EnemyPos = new Vector3(-14, 8, 0);
        }
        else if(num==1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPos = new Vector3(14, 8, 0);
        }
        CreateItem(item[3], EnemyPos, Quaternion.identity);
    }
}
