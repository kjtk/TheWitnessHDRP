using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effecttrigget : MonoBehaviour

{
    [SerializeField]
    private AudioSource audio;
    public AudioClip efekti;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player")){
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
