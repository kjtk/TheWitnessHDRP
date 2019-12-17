using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour {

    Vector3 lastPos;
    //public Transform obj;
    public float thresholdSpeed = 0f;
    Animator anim;

    void Start()
    {
        lastPos = transform.position;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        Vector3 offset = transform.position - lastPos;

        if (offset.magnitude > Time.fixedDeltaTime * thresholdSpeed) { 
            anim.Play("Walk");
        } else {
            anim.Play("Idle");

        }

        lastPos = transform.position;
    }
}

//void Update() {
//    Vector3 offset = obj.position - lastPos;
//    if (offset.x > threshold) {
//        lastPos = obj.position; // update lastPos
//                                // code to execute when X is getting bigger
//    } else
//    if (offset.x < -threshold) {
//        lastPos = obj.position; // update lastPos
//                                // code to execute when X is getting smaller 
//    }
