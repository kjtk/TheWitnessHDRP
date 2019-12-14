using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playsound : MonoBehaviour
{
    public AudioClip soundtoplay;
    public float Volume;
    AudioSource audio;
    public bool AlreadyPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter()
    {
        if(!AlreadyPlayed)
        {
            audio.PlayOneShot(soundtoplay, Volume);

            AlreadyPlayed = true;
        }
    }
    // Update is called once per frame
  
}
