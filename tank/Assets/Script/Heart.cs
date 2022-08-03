using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    private SpriteRenderer sr;

    public Sprite brokensprite;

    public GameObject explosionPrefab;
    public AudioClip dieaudio;

    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

    }

   public void Die()
    {
        sr.sprite = brokensprite;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeat = true;
        AudioSource.PlayClipAtPoint(dieaudio, transform.position);
    }
}
