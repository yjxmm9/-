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
        showEnemyNums.text = "敌人数量："+UIHealthBar.instance.fixedNum + "/7";
        if (UIHealthBar.instance.fixedNum>=7)
        {
            showEnemyNums.text = "任务完成！\n去Jambi那里报告吧！" ;
        }
    }
}
