using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Collections;

public class SnakeHead : MonoBehaviour
{
    public List<Transform> bodyList = new List<Transform>();
    public float velocity;
    public int step;
    private int x;
    private int y;
    private Vector3 headPos;
    private Transform canvas;
    private bool isDead = false;

    public AudioClip eatClip;
    public AudioClip dieClip;
    public GameObject bodyPrefab;
    public GameObject dieEffect;
    public Sprite[] bodySprites = new Sprite[2];

    void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
        //通过Resources.Load()方法加载资源，path的书写不用加Resources/以及文件扩展名
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("sh", "sh01"));
        bodySprites[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb01", "sb0101"));
        bodySprites[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb02", "sb0102"));
    }

    void Start()
    {
        InvokeRepeating("Move", 0, velocity);
        x = step; y = 0;
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&MainUIController.Instance.isPause==false&&isDead==false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity-0.15f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && MainUIController.Instance.isPause == false && isDead == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }
        if (Input.GetKey(KeyCode.W)&&y!=-step && MainUIController.Instance.isPause == false && isDead == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0; y = step;
        }
        if (Input.GetKey(KeyCode.A)&&x!=step && MainUIController.Instance.isPause == false && isDead == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step ;y = 0;
        }
        if (Input.GetKey(KeyCode.S)&&y!=step && MainUIController.Instance.isPause == false && isDead == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0; y = -step;
        }
        if (Input.GetKey(KeyCode.D)&&x!=-step && MainUIController.Instance.isPause == false && isDead == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step; y = 0;
        }
    }

    void Move()
    {
        headPos = gameObject.transform.localPosition;//保存蛇头移动前位置
        gameObject.transform.localPosition = new Vector3(headPos.x+x,headPos.y+y,headPos.z);//蛇头移动
        if (bodyList.Count>0)
        {
            for (int i = bodyList.Count-2; i >= 0; i--)//从后往前移动蛇身
            {
                bodyList[i + 1].localPosition = bodyList[i].localPosition;
            }
            bodyList[0].localPosition = headPos;
        }
    }

    void Grow()
    {
        AudioSource.PlayClipAtPoint(eatClip,new Vector3(0,0,-8));
        int index=(bodyList.Count%2==0)?0:1;
        GameObject body = Instantiate(bodyPrefab);
        body.GetComponent<Image>().sprite = bodySprites[index];
        body.transform.SetParent(canvas,false);
        bodyList.Add(body.transform);

    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(dieClip, new Vector3(0, 0, -10),2.0f);
        CancelInvoke();
        isDead = true;
        Instantiate(dieEffect);
        PlayerPrefs.SetInt("lastl", MainUIController.Instance.length);
        PlayerPrefs.SetInt("lasts", MainUIController.Instance.score);
        if (PlayerPrefs.GetInt("bests",0)<MainUIController.Instance.score)
        {
            PlayerPrefs.SetInt("bestl", MainUIController.Instance.length);
            PlayerPrefs.SetInt("bests", MainUIController.Instance.score);
        }
        StartCoroutine(GameOver(1.5f));
    }

    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Food")
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI();
            Grow();
            FoodMaker.Instance.MakeFood((Random.Range(0, 100) < 20)? true:false);
        }
        else if(collision.tag == "Body")
        {
            Die();
        }
        else if (collision.tag=="Reward")
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI(Random.Range(5,15)*10);
            Grow();
        }
        else
        {
            if (MainUIController.Instance.hasBorder)
            {
                Die();
            }
            else
            {
                switch (collision.gameObject.name)
                {
                    case "Up":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 20, transform.localPosition.z);
                        break;
                    case "Down":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 20, transform.localPosition.z);
                        break;
                    case "Left":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 140, transform.localPosition.y, transform.localPosition.z);
                        break;
                    case "Right":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 180, transform.localPosition.y, transform.localPosition.z);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
