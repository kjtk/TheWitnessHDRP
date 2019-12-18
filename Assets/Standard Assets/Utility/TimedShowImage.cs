using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedShowImage : MonoBehaviour
{
    Image img;
    float timer = 0;
    private void Awake() {
        img = GetComponent<Image>();
    }
    public void Show(float seconds) {
        img.enabled = true;
        timer = seconds;
    }
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                img.enabled = false;
            }
        }
    }
}
