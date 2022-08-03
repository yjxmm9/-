using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagezone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        RubyController rubycontroller = collision.GetComponent<RubyController>();
        if (rubycontroller != null)
        {
            //²»ÂúÑª×´Ì¬ÏÂ
            rubycontroller.ChangeHealth(-1);
            //Debug.Log(rubycontroller.Health);
        }
    }

}