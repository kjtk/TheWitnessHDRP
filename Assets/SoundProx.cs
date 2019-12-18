using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProx : MonoBehaviour {
    Transform player;
    AudioSource audio;
    public float heightAttenuationFactor;
    
    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("FirstPersonCharacter").transform;
        audio = GetComponent<AudioSource>();
    } 

    // Update is called once per frame
    void Update() 
    {
        float distanceY = Mathf.Abs(transform.position.y - player.position.y);
        audio.volume = 1 - distanceY * heightAttenuationFactor;

    }
}
