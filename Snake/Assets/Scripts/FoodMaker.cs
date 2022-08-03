using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour
{
    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }

    public int xlimit=20;
    public int ylimit=9;
    public int xoffset=8;
    public GameObject foodPrefab;
    public Sprite[] foodSprite;
    private Transform foodHolder;
    public GameObject rewardPrefab;

    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        foodHolder = GameObject.Find("FoodController").transform;
        MakeFood(false);
    }

    public void MakeFood(bool isReward)
    {
        int index = Random.Range(0, foodSprite.Length);
        GameObject food = Instantiate(foodPrefab);
        food.GetComponent<Image>().sprite = foodSprite[index];
        food.transform.SetParent(foodHolder, false);
        int x = Random.Range(-xlimit + xoffset, xlimit);
        int y = Random.Range(-ylimit, ylimit);
        food.transform.localPosition = new Vector3(x * 20, y * 20, 0);
        if (isReward)
        {
            GameObject reward = Instantiate(rewardPrefab);
            reward.transform.SetParent(foodHolder, false);
            x = Random.Range(-xlimit + xoffset, xlimit);
            y = Random.Range(-ylimit, ylimit);
            reward.transform.localPosition = new Vector3(x * 20, y * 20, 0);
        }
    }
}
