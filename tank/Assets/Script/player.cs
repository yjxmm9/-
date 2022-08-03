using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //����ֵ
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal;
    private float DefendTimeVal=3;
    private bool IsDefended=true;


    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//�� �� �� ��
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject DefendEffectPrefab;
    public AudioSource moveaudio;
    public AudioClip[] tankaudio;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //����޵�
        if(IsDefended)
        {
            DefendEffectPrefab.SetActive(true);
            DefendTimeVal -= Time.deltaTime;
            if (DefendTimeVal<=0)
            {
                IsDefended = false;
                DefendEffectPrefab.SetActive(false);
            }
        }
        //�ӵ�CD
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }


    }
    private void FixedUpdate()
    {
        if(PlayerManager.Instance.isDefeat)
        {
            return;
        }
        Move();





    }

    private void Attack()//̹�˹�������
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }


    private void Move()//̹���ƶ�����
    {
        float h = Input.GetAxisRaw("Horizontal"); //��ȡ������
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        if (Mathf.Abs(h) > 0.05f)
        {
            moveaudio.clip = tankaudio[1];
            if (!moveaudio.isPlaying)
            {
                moveaudio.Play();
            }
        }
        if (h != 0)
        {
            return;
        }
        float v = Input.GetAxisRaw("Vertical"); //��ȡ������
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, 180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        if (Mathf.Abs(v) > 0.05f)
        {
            moveaudio.clip = tankaudio[1];
            if (!moveaudio.isPlaying)
            {
                moveaudio.Play();
            }
        }
        else
        {
            moveaudio.clip = tankaudio[0];
            if (!moveaudio.isPlaying)
            {
                moveaudio.Play();
            }
        }
    }


    private void Die()//̹����������
    {
        if(IsDefended)
        {
            return;
        }

        PlayerManager.Instance.isDead = true;

        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        //����
        Destroy(gameObject);
    }
}