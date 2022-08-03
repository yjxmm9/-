using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    //����ֵ
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float v=-1;
    private float h;
    
    
    //��ʱ��
    private float timeVal;
    private float timeValChangeDirection=0;


    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//�� �� �� ��
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

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
     


        //�ӵ�CD
        if (timeVal >= 3)
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
        Move();



    }

    private void Attack()//̹�˹�������
    {
         Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
    }


    private void Move()//̹���ƶ�����
    {
        if (timeValChangeDirection >= 3)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                v = 0;
                h = 1;
            }
            else if (num > 2 && num <= 4)
            {
                v = 0;
                h = -1;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
        
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
        if (h != 0)
        {
            return;
        }
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
    }


    private void Die()//̹����������
    {

        PlayerManager.Instance.PlayerSocre++;

        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        //����
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy"|| collision.gameObject.tag == "Wall"|| collision.gameObject.tag == "barrier")
        {
            timeValChangeDirection = 4;
        }
    }
}
