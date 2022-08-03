using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barriar : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip hitaudio;
    
    private void playaudio()
    {
        AudioSource.PlayClipAtPoint(hitaudio, transform.position);
    }
}
