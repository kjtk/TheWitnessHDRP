using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DoorControl : MonoBehaviour {

    // derived from...
    // https://answers.unity.com/questions/1023743/rotating-door.html


    UnityEvent doorAction;

    public Quaternion doorOpenRotation;
    public Quaternion doorCloseRotation;

    
    public bool doorIsOpen;
    public float doorOpenSpeed;
    public float doorCloseSpeed;

    void Start() {
        
        //doorAction.AddListener(OpenDoor);
        //doorAction.AddListener(DoorDebug);

    }

    void Update() {
        if (Input.anyKeyDown && doorAction != null) {
            doorAction.Invoke();
        }
    }

    public void OpenDoor() {
        Debug.Log("openDoor");
        if (!doorIsOpen) {
            transform.rotation = Quaternion.Slerp(transform.rotation, doorOpenRotation, doorOpenSpeed);
        }
    }

    public void CloseDoor() {
        Debug.Log("closeDoor");
        if (doorIsOpen) {
            transform.rotation = Quaternion.Slerp(transform.rotation, doorCloseRotation, doorCloseSpeed);
        }
    }

    public void DoorDebug() {
        Debug.Log("Door Debug");
    }
}