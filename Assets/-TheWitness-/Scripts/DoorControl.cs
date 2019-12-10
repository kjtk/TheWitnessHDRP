using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DoorControl : MonoBehaviour {

    // DoorControl derived from...
    // https://sharpcoderblog.com/blog/unity-3d-openable-door-tutorial

    public float doorOpenAngle = 90.0f; // Direction: inwards or outwards
    public float doorOpenSpeed = 2.0f;
    
    public bool doorIsOpen = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;

    void Start() {
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;
    }

    void Update() {
        if (openTime < 1) {
            openTime += Time.deltaTime * doorOpenSpeed;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (doorIsOpen ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);
    }

    public void OpenDoor() {
        //doorIsOpen = !doorIsOpen;
        doorIsOpen = true;
        currentRotationAngle = transform.localEulerAngles.y;
        openTime = 0;
    }

    public void CloseDoor() {
        //doorIsOpen = !doorIsOpen;
        doorIsOpen = false;
        currentRotationAngle = transform.localEulerAngles.y;
        openTime = 0;
    }

    public void DebugDoor() {
        Debug.Log("Debug Door");
    }
}