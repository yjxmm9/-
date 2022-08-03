using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rigidbody2d;
    private Animator animator;

    public bool vertical;//�������
    private int direction=1;//�������
    public float changeTime = 3.0f;//����ı�ʱ��������
    private float timer;//��ʱ��

    private bool broken;//��ǰ�������Ƿ����

    public ParticleSystem smokeEffect;

    private AudioSource audioSource;

    public AudioClip fixedSound;
    public AudioClip[] hitSounds;

    public GameObject hitEffectParticle;


    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        //animator.SetFloat("MoveX", direction);
        //animator.SetBool("Vertical", vertical);
        PlayMoveAnimation();
        broken = true;
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        timer -= Time.fixedDeltaTime;
        if (timer<0)
        {
            direction = -direction;
            //animator.SetFloat("MoveX", direction);
            PlayMoveAnimation();
            timer = changeTime;
        }
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y += Time.fixedDeltaTime * speed*direction;
        }
        else
        {
            position.x += Time.fixedDeltaTime * speed*direction;
        }
        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController rubyController = collision.gameObject.GetComponent<RubyController>();
        if (rubyController!=null)
        {
            rubyController.ChangeHealth(-1);
        }
    }

    private void PlayMoveAnimation()//�����ƶ������ķ���
    {
        if (vertical)//��ֱ���򶯻�����
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else//ˮƽ���򶯻�����
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
    }
    public void Fix()
    {
        Instantiate(hitEffectParticle, transform.position, Quaternion.identity);
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        audioSource.volume = 0.5f;
        audioSource.Stop();
        int randomNum = Random.Range(0, 2);
        audioSource.PlayOneShot(hitSounds[randomNum]);
        Invoke("PlayFixedSound",0.4f);
        UIHealthBar.instance.fixedNum++;
    }

    private void PlayFixedSound()
    {
        audioSource.PlayOneShot(fixedSound);
    }
}
