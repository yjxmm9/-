using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnemys : MonoBehaviour
{
    // Start is called before the first frame update
    public Text showEnemyNums;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        showEnemyNums.text = "����������"+UIHealthBar.instance.fixedNum + "/7";
        if (UIHealthBar.instance.fixedNum>=7)
        {
            showEnemyNums.text = "������ɣ�\nȥJambi���ﱨ��ɣ�" ;
        }
    }
}
