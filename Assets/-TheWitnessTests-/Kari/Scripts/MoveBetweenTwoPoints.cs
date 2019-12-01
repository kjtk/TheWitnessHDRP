using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenTwoPoints : MonoBehaviour {
    public Vector3 position1 = new Vector3(-4, 0, 0);
    public Vector3 position2 = new Vector3(4, 0, 0);
    public float speed = 0.1f;

    void Update() {
        // https://answers.unity.com/questions/690884/how-to-move-an-object-along-x-axis-between-two-poi.html
        //transform.position = Vector3.Lerp(position1, position2, Mathf.PingPong(Time.time * speed, 1.0f));
        // more natural
        transform.position = Vector3.Lerp(position1, position2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }
}