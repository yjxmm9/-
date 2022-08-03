using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public int maxHealth = 5;//�������ֵ
    private int currentHealth;//��ǰ����ֵ
    public int speed=3;//ruby�ٶ�
    
    //Ruby�޵�ʱ��
    public float timeInvincible = 2.0f;//�޵�ʱ�䳣��
    private bool isInvincible;
    private float invincibleTimer;//��ʱ��

    private Vector2 lookDirection = new Vector2(1, 0);

    private Animator animator;

    public GameObject projectilePrefab;

    public AudioSource audioSource;
    public AudioSource walkAudioSource;

    public AudioClip playerHit;
    public AudioClip attackSoundClip;
    public AudioClip walkSound;

    private Vector3 respawnPosition;


    public int Health { get { return currentHealth; } } 

    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x,0)||!Mathf.Approximately(move.y, 0))//��ǰ��������ĳ������Ϊ0
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            if (!walkAudioSource.isPlaying)
            {
                walkAudioSource.clip = walkSound;
                walkAudioSource.Play();
            }
        }
        else
        {
            walkAudioSource.Stop();
        }
        //���߶����Ŀ���
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        //�ƶ�
        Vector2 position = transform.position;
        //position.x = position.x +speed*horizontal * Time.fixedDeltaTime;
        //position.y = position.y +speed*vertical * Time.fixedDeltaTime;
        position=position+ speed *move* Time.fixedDeltaTime;//Rubyλ���ƶ�
        //transform.position = position;//����box collider2d�໥��ײ���¶���
        rigidbody2d.MovePosition(position);//��rigidbody2dȥ��ײ�������
        //�޵�ʱ�����    
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer<=0)
            {
                isInvincible = false;
            }
        }
        
    }

    private void Update()
    {
        //����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        //����Ƿ���NPC�Ի�
        if (Input.GetKeyDown(KeyCode.T))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position+Vector2.up*0.2f,lookDirection,1.5f,LayerMask.GetMask("NPC"));
            if (hit.collider!=null)
            {
                NPCDialog npcDialog = hit.collider.GetComponent<NPCDialog>();
                if (npcDialog!=null)
                {
                    npcDialog.DisplayDialog();
                }
            }
        }
    }


    public void ChangeHealth(int amount)
    {
        if (amount<0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
            PlaySound(playerHit);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);//������0��5�ķ�Χ��
        if (currentHealth<=0)
        {
            Respawn();
        }
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    private void Launch()
    {
        if (!UIHealthBar.instance.hasTask)
        {
            return;
        }
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position+Vector2.up*0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
        PlaySound(attackSoundClip);
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip,0.25f);
    }

    private void Respawn()
    {
        ChangeHealth(maxHealth);
        transform.position = respawnPosition;
    }
}
