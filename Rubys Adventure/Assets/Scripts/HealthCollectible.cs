using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip audioClip;

    public GameObject effectParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController rubycontroller = collision.GetComponent<RubyController>();
        if(rubycontroller!=null)
        {
            if (rubycontroller.Health<rubycontroller.maxHealth)
            {
                //²»ÂúÑª×´Ì¬ÏÂ
                rubycontroller.ChangeHealth(1);
                rubycontroller.PlaySound(audioClip);
                //Debug.Log(rubycontroller.Health);
                Instantiate(effectParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }
        
    }
}
