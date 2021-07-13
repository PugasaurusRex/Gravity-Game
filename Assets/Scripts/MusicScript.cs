using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    AudioSource Speaker;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        Speaker = GetComponent<AudioSource>();
        Speaker.volume = PlayerPrefs.GetFloat("volume", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Speaker.isPlaying)
        {
            int temp = Random.Range(0, clips.Length);
            Speaker.clip = clips[temp];
            Speaker.PlayOneShot(Speaker.clip);
        }
    }
}
