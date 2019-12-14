using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanager : MonoBehaviour
{
    public AudioSource src;
    public void PlaySound()
    {

        src.Play();
    }
    public void PauseSound()
    {

        src.Pause();
    }
    public void StopSound()
    {

        src.Stop();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

